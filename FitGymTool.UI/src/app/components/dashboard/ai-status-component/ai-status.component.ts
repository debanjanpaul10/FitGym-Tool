import {
  Component,
  OnInit,
  OnDestroy,
  inject,
  ChangeDetectorRef,
} from '@angular/core';
import { AgentStatusService } from '@services/agent-status.service';
import { Ripple } from 'primeng/ripple';
import * as signalR from '@microsoft/signalr';
import { AgentStatus } from '@models/interfaces/agent-status.interface';

@Component({
  selector: 'app-ai-status-component',
  imports: [Ripple],
  templateUrl: './ai-status.component.html',
  styleUrl: './ai-status.component.scss',
})
export class AiStatusComponent implements OnInit, OnDestroy {
  protected statusText: string = 'Offline';
  protected isOnline: boolean = false;
  private statusCheckId: any | null = null;
  private readonly _agentStatusService = inject(AgentStatusService);
  private readonly _cdr = inject(ChangeDetectorRef);

  ngOnInit(): void {
    this.initializeComponent();
  }

  ngOnDestroy(): void {
    this.cleanup();
  }

  private async initializeComponent(): Promise<void> {
    this.updateDisplay();
    this.startPeriodicStatusCheck();

    try {
      await this._agentStatusService.startConnection();
      await this.loadInitialStatus();
      this.setupStatusListener();
    } catch (error) {
      console.error(error);
    }
  }

  private async loadInitialStatus(): Promise<void> {
    try {
      const response: any = await this._agentStatusService.getCurrentStatus();
      this.updateStatus(response.isAvailable);
    } catch (error) {
      console.error(error);
    }
  }

  private setupStatusListener(): void {
    this._agentStatusService.addAgentStatusListener((status: AgentStatus) => {
      this.updateStatus(status.isAvailable);
    });
  }

  private startPeriodicStatusCheck(): void {
    this.statusCheckId = setInterval(async () => {
      try {
        const response: any = await this._agentStatusService.getCurrentStatus();
        this.updateStatus(response.isAvailable);
      } catch (error) {
        console.error(error);
        const connectionState = this._agentStatusService.getConnectionState();
        if (connectionState === signalR.HubConnectionState.Disconnected) {
          try {
            await this._agentStatusService.startConnection();
            this.setupStatusListener();
          } catch (reconnectError) {
            console.error(reconnectError);
          }
        }

        this.updateStatus(false);
      }
    }, 10000);
  }

  private updateStatus(isOnline: boolean): void {
    const previousStatus = this.isOnline;
    this.isOnline = isOnline;

    if (previousStatus !== isOnline) {
      this.updateDisplay();
      this._cdr.detectChanges();
    }
  }

  private updateDisplay(): void {
    this.statusText = this.isOnline ? 'Active' : 'Offline';
  }

  private cleanup(): void {
    if (this.statusCheckId) {
      clearInterval(this.statusCheckId);
      this.statusCheckId = null;
    }
  }
}
