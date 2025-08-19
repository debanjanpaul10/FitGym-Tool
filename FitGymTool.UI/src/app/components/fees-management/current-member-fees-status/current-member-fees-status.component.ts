import { CommonModule } from '@angular/common';
import {
  AfterViewChecked,
  Component,
  ElementRef,
  inject,
  Input,
  OnDestroy,
  OnInit,
  signal,
  ViewChild,
  WritableSignal,
} from '@angular/core';
import { TableModule } from 'primeng/table';
import { Skeleton } from 'primeng/skeleton';
import { Chart, registerables } from 'chart.js';
import { Ripple } from 'primeng/ripple';
// Register Chart.js components
Chart.register(...registerables);

import { ToasterService } from '@core/services/toaster.service';
import { CurrentMembersFeesStatusDTO } from '@models/DTO/current-members-fees-status-dto.model';
import { ResponseDto } from '@models/DTO/response-dto.model';
import { MemberFeesApiService } from '@services/member-fees-api.service';
import { MasterMappingDataDto } from '@models/DTO/Mapping/master-mapping-dto.model';
import { ChartConstants } from '@shared/application.constants';
import { FeesManagementService } from '@core/services/fees-management-service.service';

@Component({
  selector: 'app-current-member-fees-status',
  imports: [CommonModule, TableModule, Skeleton, Ripple],
  templateUrl: './current-member-fees-status.component.html',
  styleUrl: './current-member-fees-status.component.scss',
})
export class CurrentMemberFeesStatusComponent
  implements OnInit, AfterViewChecked, OnDestroy
{
  @Input() mappingMasterData: MasterMappingDataDto = new MasterMappingDataDto();

  @ViewChild('feesStatusChart', { static: false })
  feesStatusChartCanvas!: ElementRef<HTMLCanvasElement>;

  protected memberFeesData: WritableSignal<CurrentMembersFeesStatusDTO[]> =
    signal([]);
  protected isMembersFeesDataLoading: WritableSignal<boolean> = signal(false);
  protected chartConstants =
    ChartConstants.CurrentMemberFeesStatusChartConstants;
  protected counts = {
    paidCount: 0,
    dueCount: 0,
    overdueCount: 0,
    toBeCancelledCount: 0,
  };

  private feesStatusChart: Chart | null = null;
  private chartInitialized: boolean = false;
  protected colors = ['#7CFC98', '#FFD700', '#FF7C7C', '#D3D3D3'];

  private readonly memberFeesApiService: MemberFeesApiService =
    inject(MemberFeesApiService);
  private readonly toasterService: ToasterService = inject(ToasterService);
  private readonly _feesManagementService: FeesManagementService = inject(
    FeesManagementService
  );

  ngOnInit(): void {
    this.getCurrentMembersFeesStatus();
  }

  ngAfterViewChecked(): void {
    if (
      !this.chartInitialized &&
      this.feesStatusChartCanvas?.nativeElement &&
      this.memberFeesData().length > 0
    ) {
      this.createChart();
      this.chartInitialized = true;
    }
  }

  ngOnDestroy(): void {
    this.feesStatusChart?.destroy();
    this.chartInitialized = false;
  }

  /**
   * Fetches current members fees status data from the API.
   * Updates the component state and chart with the received data.
   */
  public getCurrentMembersFeesStatus(): void {
    this.isMembersFeesDataLoading.set(true);

    this.memberFeesApiService.GetCurrentMembersFeesStatusAsync().subscribe({
      next: (response: ResponseDto) => {
        if (response?.isSuccess && response?.responseData) {
          this.memberFeesData.set(response?.responseData);
          this._feesManagementService.currentMemberFees =
            response?.responseData;

          // Force chart recreation by resetting the initialization flag
          this.chartInitialized = false;

          // Update chart data with a delay to ensure DOM is ready
          setTimeout(() => {
            this.updateChartDataFromMembers(response.responseData);
          }, 100);
        } else {
          this.toasterService.showError(response?.responseData);
        }
      },
      error: (err: Error) => {
        this.isMembersFeesDataLoading.set(false);
        console.error(err.message);
        this.toasterService.showError(err.message);
      },
      complete: () => {
        this.isMembersFeesDataLoading.set(false);
      },
    });
  }

  /**
   * Creates and initializes the Chart.js doughnut chart with fees payment status statistics.
   *
   * This method configures a doughnut chart that displays the distribution of members
   * by their fees payment status. The chart shows four categories: "Paid", "Due",
   * "Overdue", and "To Be Cancelled" with appropriate colors for each status.
   */
  private createChart(): void {
    if (this.feesStatusChart) {
      this.feesStatusChart.destroy();
      this.feesStatusChart = null;
    }

    const labels =
      this.mappingMasterData?.feesPaymentStatusMapping?.length > 0
        ? this.mappingMasterData.feesPaymentStatusMapping.map(
            (x) => x.statusName
          )
        : ['Paid', 'Due', 'Overdue', 'To Be Cancelled'];

    const customLabels = [...labels];

    if (
      this.feesStatusChartCanvas &&
      this.feesStatusChartCanvas.nativeElement
    ) {
      this.feesStatusChart = new Chart(
        this.feesStatusChartCanvas.nativeElement,
        {
          type: 'pie',
          data: {
            datasets: [
              {
                label: this.chartConstants.SubHeader,
                data: [
                  this.counts.paidCount,
                  this.counts.dueCount,
                  this.counts.overdueCount,
                  this.counts.toBeCancelledCount,
                ],
                backgroundColor: this.colors,
                borderWidth: 1,
              },
            ],
          },
          options: {
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
              legend: {
                position: 'bottom',
                labels: {
                  padding: 20,
                  font: {
                    family: 'Cascadia Mono',
                  },
                },
              },
              tooltip: {
                callbacks: {
                  label: (context): string => {
                    const labelIndex = context.dataIndex;
                    const value = context.parsed;
                    return `${customLabels[labelIndex]}: ${value}`;
                  },
                },
                titleFont: {
                  family: 'Cascadia Mono',
                },
                bodyFont: {
                  family: 'Cascadia Mono',
                },
              },
            },
          },
        }
      );
    }
  }

  /**
   * Updates the chart data based on the feesPaymentStatus values from the API response.
   * Counts members in each payment status category and updates the chart.
   */
  private updateChartDataFromMembers(
    feesStatus: CurrentMembersFeesStatusDTO[]
  ): void {
    this.counts.paidCount = 0;
    this.counts.dueCount = 0;
    this.counts.overdueCount = 0;
    this.counts.toBeCancelledCount = 0;

    const statusNames =
      this.mappingMasterData?.feesPaymentStatusMapping?.map(
        (x) => x.statusName
      ) || [];
    const paidStatus = statusNames[0] || 'Paid';
    const dueStatus = statusNames[1] || 'Due';
    const overdueStatus = statusNames[2] || 'Overdue';
    const toBeCancelledStatus = statusNames[3] || 'To Be Cancelled';

    // Count members by fees payment status
    for (const fee of feesStatus) {
      switch (fee.feesPaymentStatus) {
        case paidStatus:
          this.counts.paidCount++;
          break;
        case dueStatus:
          this.counts.dueCount++;
          break;
        case overdueStatus:
          this.counts.overdueCount++;
          break;
        case toBeCancelledStatus:
          this.counts.toBeCancelledCount++;
          break;
      }
    }

    this.ensureChartIsUpdated();
  }

  /**
   * Ensures the chart is properly created and updated with current data
   */
  private ensureChartIsUpdated(retryCount: number = 0): void {
    const MAX_RETRIES = 10;
    if (retryCount >= MAX_RETRIES) {
      return;
    }

    setTimeout(() => {
      if (this.feesStatusChartCanvas?.nativeElement) {
        if (this.feesStatusChart && this.chartInitialized) {
          this.feesStatusChart.data.datasets[0].data = [
            this.counts.paidCount,
            this.counts.dueCount,
            this.counts.overdueCount,
            this.counts.toBeCancelledCount,
          ];
          this.feesStatusChart.update();
        } else {
          if (this.feesStatusChart) {
            this.feesStatusChart.destroy();
            this.feesStatusChart = null;
          }
          this.createChart();
          this.chartInitialized = true;
        }
      } else {
        setTimeout(() => this.ensureChartIsUpdated(retryCount + 1));
      }
    }, 150);
  }
}
