import {
  Component,
  ViewChild,
  ElementRef,
  OnDestroy,
  AfterViewChecked,
  inject,
  OnInit,
  WritableSignal,
  signal,
} from '@angular/core';
import Chart from 'chart.js/auto';
import { CommonModule } from '@angular/common';

import { ChartConstants } from '@shared/application.constants';
import { CommonService } from '@core/services/common.service';
import { MasterMappingDataDto } from '@models/DTO/Mapping/master-mapping-dto.model';
import { FeesPaymentStatusMappingDto } from '@models/DTO/Mapping/fees-payment-status-mapping-dto.model';
import { MemberFeesApiService } from '@services/member-fees-api.service';
import { ToasterService } from '@services/toaster.service';
import { ResponseDto } from '@models/DTO/response-dto.model';
import { CurrentMonthFeesAndRevenueStatus } from '@models/DTO/current-month-fees-revenue-status.model';
import { SkeletonModule } from 'primeng/skeleton';

/**
 * CurrentRevenueComponent displays a horizontal bar chart representing the current revenue status.
 * It uses Chart.js to visualize paid, unpaid, and cancelled subscription fees for the current month.
 * The chart is styled to match the application's theme and uses constants for labels and colors.
 */
@Component({
  selector: 'app-current-revenue-component',
  imports: [CommonModule, SkeletonModule],
  templateUrl: './current-revenue.component.html',
  styleUrl: './current-revenue.component.scss',
})
export class CurrentRevenueComponent
  implements AfterViewChecked, OnDestroy, OnInit
{
  @ViewChild('revenueChartCanvas', { static: false })
  revenueChartCanvas!: ElementRef<HTMLCanvasElement>;

  public chartConstants = ChartConstants.RevenueChartConstants;
  public feesPaymentStatusMapping: FeesPaymentStatusMappingDto[] = [];
  public chartLabels: string[] = [];
  public isLoading: WritableSignal<boolean> = signal(true);
  public currentFeesAndRevenueData: WritableSignal<
    CurrentMonthFeesAndRevenueStatus[]
  > = signal([]);

  private revenueChart: Chart | null = null;
  private chartInitialized: boolean = false;
  private mappingMasterDataSubscription: any;
  private colors = ['#7CFC98', '#FFD700', '#FF7C7C', '#D3D3D3'];
  private labelsForTooltip: string[] = [];

  private readonly commonService: CommonService = inject(CommonService);
  private readonly memberFeesApiService: MemberFeesApiService =
    inject(MemberFeesApiService);
  private readonly toasterService: ToasterService = inject(ToasterService);

  /**
   * Angular lifecycle hook that is called after component initialization.
   * Initiates fetching of current revenue data and subscribes to mapping master data updates.
   */
  ngOnInit(): void {
    this.getCurrentRevenueData();

    this.mappingMasterDataSubscription =
      this.commonService.MappingMasterData.subscribe(
        (data: MasterMappingDataDto | null) => {
          if (data && data?.feesPaymentStatusMapping) {
            this.feesPaymentStatusMapping = data?.feesPaymentStatusMapping;
            this.chartLabels = this.feesPaymentStatusMapping.map(
              (item) => item.statusName
            );
            this.createOrUpdateChart();
          }
        }
      );
  }

  /**
   * Angular lifecycle hook that is called after the view has been checked.
   * Ensures the chart is initialized once the canvas is available.
   */
  ngAfterViewChecked(): void {
    if (!this.chartInitialized && this.revenueChartCanvas?.nativeElement) {
      this.createOrUpdateChart();
      this.chartInitialized = true;
    }
  }

  /**
   * Angular lifecycle hook that is called when the component is destroyed.
   * Cleans up the chart and unsubscribes from subscriptions.
   */
  ngOnDestroy(): void {
    this.revenueChart?.destroy();
    this.chartInitialized = false;
    if (this.mappingMasterDataSubscription) {
      this.mappingMasterDataSubscription.unsubscribe();
    }
  }

  /**
   * Gets the legend colour for a given index.
   * @param index The index number of the labels array.
   * @returns The colour value as a string.
   */
  public getLegendColor(index: number): string {
    const colors = [...this.colors];
    return colors[index % colors.length];
  }

  // #region PRIVATE METHODS

  /**
   * Creates or updates the Chart.js horizontal bar chart for revenue data.
   * Uses the processed API response for labels and data.
   */
  private createOrUpdateChart(): void {
    if (!this.revenueChartCanvas) {
      return;
    }
    const labels = this.chartLabels;
    this.labelsForTooltip = [...labels];
    const data = this.getChartDataFromApi();
    if (this.revenueChart) {
      this.revenueChart.destroy();
      this.revenueChart = null;
      this.createOrUpdateChart();
      return;
    } else {
      const labelsForTooltip = this.labelsForTooltip;
      this.revenueChart = new Chart(this.revenueChartCanvas.nativeElement, {
        type: 'bar',
        data: {
          labels: labels,
          datasets: [
            {
              data: data,
              backgroundColor: this.colors,
            },
          ],
        },
        options: {
          indexAxis: 'y',
          plugins: {
            legend: {
              display: false,
            },
            title: {
              color: '#f1f1f1',
              font: {
                family: 'Cascadia Mono',
              },
            },
            tooltip: {
              callbacks: {
                title: () => [],
                label: (context: any): string => {
                  const labelIndex = context.dataIndex;
                  const label =
                    labelsForTooltip[labelIndex] ?? `Label ${labelIndex + 1}`;
                  const value = context.parsed?.x ?? context.raw ?? '';
                  return `${label}: ${value}`;
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
          scales: {
            x: {
              ticks: {
                color: '#f1f1f1',
                font: {
                  family: 'Cascadia Mono',
                },
              },
              grid: {
                color: 'rgba(241,241,241,0.2)',
              },
            },
            y: {
              ticks: {
                color: '#f1f1f1',
                font: {
                  family: 'Cascadia Mono',
                },
              },
              grid: {
                color: 'rgba(241,241,241,0.2)',
              },
            },
          },
        },
      });
    }
  }

  /**
   * Fetches the current month's revenue and fees data from the API and updates the chart.
   * Handles loading state and error notifications.
   */
  private getCurrentRevenueData(): void {
    this.isLoading.set(true);

    this.memberFeesApiService
      .GetCurrentMonthFeesAndRevenueStatusAsync()
      .subscribe({
        next: (response: ResponseDto) => {
          if (response && response?.isSuccess) {
            this.currentFeesAndRevenueData.set(response.responseData);
            this.createOrUpdateChart();
          } else {
            this.toasterService.showError(response?.responseData);
          }
        },
        error: (error: Error) => {
          this.isLoading.set(false);
          console.error(error);
          this.toasterService.showError(error?.message);
        },
        complete: () => {
          this.isLoading.set(false);
        },
      });
  }

  /**
   * Processes the API data to group and sum the amount by feesStatus.
   * Ensures the data order matches the chart labels.
   * @returns An array of numbers representing the summed amounts for each feesStatus.
   */
  private getChartDataFromApi(): number[] {
    const dataMap: { [status: string]: number } = {};
    const dataArray = this.currentFeesAndRevenueData() || [];
    dataArray.forEach((item) => {
      const status = item.feesStatus;
      const amount = item.amount || 0;
      if (!dataMap[status]) {
        dataMap[status] = 0;
      }
      dataMap[status] += amount;
    });
    return this.chartLabels.map((label) => dataMap[label] || 0);
  }
  // #endregion
}
