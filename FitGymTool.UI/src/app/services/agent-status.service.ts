import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';

import { ApiRoutes } from '@shared/routes.constants';

/**
 * Service responsible for managing real-time communication with the AI agent status hub.
 * Provides functionality to establish SignalR connections, handle automatic reconnection,
 * and retrieve current agent availability status. Implements robust error handling and
 * exponential backoff reconnection strategies to ensure reliable communication.
 */
@Injectable({
  providedIn: 'root',
})
export class AgentStatusService {
  private _hubConnection: signalR.HubConnection;
  private _reconnectAttempts = 0;
  private _maxReconnectAttempts = 5;
  private _reconnectDelay = 5000; // 5 seconds
  private _reconnectTimeoutId: any = null;

  constructor() {
    this._hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(ApiRoutes.CommonApi.GetAgentStatus_ApiRoute, {
        withCredentials: false,
      })
      .withAutomaticReconnect([0, 2000, 10000, 30000]) // Built-in reconnect with delays
      .build();

    this.setupConnectionHandlers();
  }

  /**
   * Establishes a SignalR connection to the agent status hub.
   * Resets reconnection attempt counter on successful connection and schedules
   * reconnection attempts if the initial connection fails.
   * @throws Error when connection establishment fails
   */
  public async startConnection(): Promise<void> {
    try {
      if (
        this._hubConnection.state === signalR.HubConnectionState.Disconnected
      ) {
        await this._hubConnection.start();
        this._reconnectAttempts = 0; // Reset attempts on successful connection
      }
    } catch (error) {
      console.error(error);
      this.scheduleReconnect();
      throw error;
    }
  }

  /**
   * Registers a callback function to receive real-time agent status updates via SignalR.
   * The callback will be invoked whenever the server broadcasts agent status changes.
   * @param callback Function to execute when agent status updates are received
   */
  public addAgentStatusListener(callback: (status: any) => void): void {
    this._hubConnection.on('ReceiveAgentStatus', callback);
  }

  /**
   * Retrieves the current agent availability status from the server.
   * Automatically attempts reconnection if the connection is in a disconnected state.
   * @returns Promise resolving to the current agent status information
   * @throws Error when SignalR connection is not established
   */
  public getCurrentStatus(): Promise<any> {
    if (this._hubConnection.state === signalR.HubConnectionState.Connected) {
      return this._hubConnection.invoke('GetCurrentStatus');
    } else {
      // Try to reconnect if disconnected
      if (
        this._hubConnection.state === signalR.HubConnectionState.Disconnected
      ) {
        this.startConnection().catch(() => {}); // Silent reconnection attempt
      }
      return Promise.reject(new Error('SignalR connection not established'));
    }
  }

  /**
   * Returns the current state of the SignalR hub connection.
   * Useful for monitoring connection health and implementing connection-dependent logic.
   * @returns Current SignalR hub connection state
   */
  public getConnectionState(): signalR.HubConnectionState {
    return this._hubConnection.state;
  }

  /**
   * Forces a complete reconnection by stopping the current connection and establishing a new one.
   * Useful for recovering from persistent connection issues or when manual reconnection is required.
   * @throws Error when forced reconnection fails
   */
  public async forceReconnect(): Promise<void> {
    try {
      if (
        this._hubConnection.state !== signalR.HubConnectionState.Disconnected
      ) {
        await this._hubConnection.stop();
      }
      await this.startConnection();
    } catch (error) {
      console.error('Force reconnect failed:', error);
      throw error;
    }
  }

  // #region PRIVATE METHODS

  /**
   * Configures event handlers for SignalR connection lifecycle events.
   * Handles automatic reconnection success, connection closure, and reconnection attempts.
   * Resets reconnection counters and clears timeouts on successful reconnection.
   */
  private setupConnectionHandlers(): void {
    // Handle successful reconnection
    this._hubConnection.onreconnected(() => {
      this._reconnectAttempts = 0;
      this.clearReconnectTimeout();
    });

    // Handle connection closure
    this._hubConnection.onclose((error) => {
      console.error(error);
      this.scheduleReconnect();
    });

    // Handle reconnection attempts
    this._hubConnection.onreconnecting((error) => {
      console.error(error);
    });
  }

  /**
   * Schedules a reconnection attempt using exponential backoff strategy.
   * Stops scheduling additional attempts once the maximum reconnection limit is reached.
   * Uses progressive delay calculation to avoid overwhelming the server with connection requests.
   */
  private scheduleReconnect(): void {
    if (this._reconnectAttempts >= this._maxReconnectAttempts) {
      console.error(
        `Max reconnection attempts (${this._maxReconnectAttempts}) reached. Stopping reconnection attempts.`
      );
      return;
    }

    this.clearReconnectTimeout();

    const delay = this._reconnectDelay * Math.pow(2, this._reconnectAttempts); // Exponential backoff
    this._reconnectTimeoutId = setTimeout(async () => {
      this._reconnectAttempts++;
      try {
        await this.startConnection();
      } catch (error) {
        console.error(error);
      }
    }, delay);
  }

  /**
   * Clears any pending reconnection timeout to prevent duplicate reconnection attempts.
   * Ensures clean state management when reconnection is successful or manually cancelled.
   */
  private clearReconnectTimeout(): void {
    if (this._reconnectTimeoutId) {
      clearTimeout(this._reconnectTimeoutId);
      this._reconnectTimeoutId = null;
    }
  }

  // #endregion
}
