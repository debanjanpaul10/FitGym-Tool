import {
  Component,
  OnInit,
  OnDestroy,
  inject,
  ChangeDetectorRef,
} from '@angular/core';
import { AgentStatusService } from '@services/agent-status.service';
import { Utilities } from '@core/helpers/utilities-helper';
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
  protected chartPath: string = '';
  protected isOnline: boolean = false;

  private chartAnimationId: any | null = null;
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
    this.startChartAnimation();
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
    // Check status every 10 seconds as fallback
    this.statusCheckId = setInterval(async () => {
      try {
        const response: any = await this._agentStatusService.getCurrentStatus();
        this.updateStatus(response.isAvailable);
      } catch (error) {
        console.error(error);
        // Check if SignalR is disconnected and try to reconnect
        const connectionState = this._agentStatusService.getConnectionState();
        if (connectionState === signalR.HubConnectionState.Disconnected) {
          try {
            await this._agentStatusService.startConnection();
            this.setupStatusListener(); // Re-setup listener after reconnection
          } catch (reconnectError) {
            console.error(reconnectError);
          }
        }

        // If we can't reach the service, assume offline
        this.updateStatus(false);
      }
    }, 10000);
  }

  private updateStatus(isOnline: boolean): void {
    const previousStatus = this.isOnline;
    this.isOnline = isOnline;

    if (previousStatus !== isOnline) {
      this.updateDisplay();
      this._cdr.detectChanges(); // Force change detection
    }
  }

  private updateDisplay(): void {
    this.statusText = this.isOnline ? 'Active' : 'Offline';
    this.chartPath = Utilities.generateStatusChartPath(this.isOnline);
  }

  private startChartAnimation(): void {
    this.chartAnimationId = setInterval(() => {
      this.chartPath = Utilities.generateStatusChartPath(this.isOnline, true);
      this._cdr.detectChanges(); // Ensure chart updates are detected
    }, 5000);
  }

  private cleanup(): void {
    if (this.chartAnimationId) {
      clearInterval(this.chartAnimationId);
      this.chartAnimationId = null;
    }

    if (this.statusCheckId) {
      clearInterval(this.statusCheckId);
      this.statusCheckId = null;
    }
  }
}
