import {
  Component,
  ElementRef,
  inject,
  OnDestroy,
  OnInit,
  signal,
  ViewChild,
  WritableSignal,
  AfterViewChecked,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import Chart from 'chart.js/auto';
import { SkeletonModule } from 'primeng/skeleton';

import { ChartConstants } from '@shared/application.constants';
import { MembersApiService } from '@services/members-api.service';
import { MemberDetailsDto } from '@models/DTO/memberdetails-dto.model';
import { ResponseDto } from '@models/DTO/response-dto.model';
import { ToasterService } from '@services/toaster.service';

/**
 * Component responsible for displaying active members statistics in a doughnut chart format.
 *
 * This component creates and manages a Chart.js doughnut chart that visualizes the distribution
 * of gym members by their status (Active vs On Termination). The chart is rendered on a canvas
 * element and provides interactive tooltips with member count information.
 *
 * Features:
 * - Displays member status distribution in a doughnut chart
 * - Provides hover tooltips with detailed count information
 * - Uses custom styling with Cascadia Mono font
 * - Automatically cleans up chart resources on component destruction
 */
@Component({
  selector: 'app-active-members-component',
  imports: [CommonModule, SkeletonModule],
  templateUrl: './active-members.component.html',
  styleUrl: './active-members.component.scss',
})
export class ActiveMembersComponent
  implements OnDestroy, OnInit, AfterViewChecked
{
  @ViewChild('activeUsersChartCanvas', { static: false })
  activeUsersChartCanvas!: ElementRef<HTMLCanvasElement>;

  public chartConstants = ChartConstants.ActiveUsersChartConstants;
  public activeCount: number = 0;
  public terminationCount: number = 0;
  public expiredCount: number = 0;
  public memberDetails: WritableSignal<MemberDetailsDto[] | []> = signal([]);
  public isLoading: WritableSignal<boolean> = signal(true);

  private activeUsersChart: Chart | null = null;
  private chartInitialized = false;

  private membersApiService: MembersApiService = inject(MembersApiService);
  private toasterService: ToasterService = inject(ToasterService);

  ngOnInit(): void {
    this.getAllMembersData();
  }

  ngAfterViewChecked(): void {
    if (!this.chartInitialized && this.activeUsersChartCanvas?.nativeElement) {
      this.createChart();
      this.chartInitialized = true;
    }
  }

  ngOnDestroy(): void {
    this.activeUsersChart?.destroy();
    this.chartInitialized = false;
  }

  /**
   * Creates and initializes the Chart.js doughnut chart with member statistics.
   *
   * This method configures a doughnut chart that displays the distribution of gym members
   * by their status. The chart shows two categories: "Active" members (green) and
   * "On Termination" members (red). The chart includes custom tooltips that display
   * the member count when hovering over segments.
   *
   * Chart Configuration:
   * - Type: Doughnut chart for clear visual representation of proportions
   * - Data: Static data showing 50 active members and 2 members on termination
   * - Colors: Green (#7CFC98) for active, Red (#FF7C7C) for termination
   * - Tooltips: Custom callback to display member counts with descriptive labels
   * - Font: Cascadia Mono for consistent typography
   */
  private createChart(): void {
    // Destroy existing chart if it exists
    if (this.activeUsersChart) {
      this.activeUsersChart.destroy();
      this.activeUsersChart = null;
    }

    const customLabels = [
      this.chartConstants.Labels.Active.yAxis,
      this.chartConstants.Labels.OnTermination.yAxis,
      this.chartConstants.Labels.Expired.yAxis,
    ];

    if (
      this.activeUsersChartCanvas &&
      this.activeUsersChartCanvas.nativeElement
    ) {
      this.activeUsersChart = new Chart(
        this.activeUsersChartCanvas.nativeElement,
        {
          type: 'doughnut',
          data: {
            labels: [
              this.chartConstants.Labels.Active.legend,
              this.chartConstants.Labels.OnTermination.legend,
              this.chartConstants.Labels.Expired.legend,
            ],
            datasets: [
              {
                label: this.chartConstants.SubHeader,
                data: [
                  this.activeCount,
                  this.terminationCount,
                  this.expiredCount,
                ],
                backgroundColor: ['#7CFC98', '#FFD27C', '#FF7C7C'],
              },
            ],
          },
          options: {
            responsive: true,
            maintainAspectRatio: false,
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
                  label: (context: any): string => {
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
   * Updates the chart data based on the membershipStatus values from the API response.
   * membershipStatus: 1 = Active, 2 = On Termination, 3 = Expired
   */
  private updateChartDataFromMembers(members: MemberDetailsDto[]): void {
    let active = 0;
    let onTermination = 0;
    let expired = 0;
    for (const member of members) {
      switch (member.membershipStatus) {
        case this.chartConstants.Labels.Active.legend:
          active++;
          break;
        case this.chartConstants.Labels.OnTermination.legend:
          onTermination++;
          break;
        case this.chartConstants.Labels.Expired.legend:
          expired++;
          break;
      }
    }
    this.activeCount = active;
    this.terminationCount = onTermination;
    this.expiredCount = expired;
    // If the chart is already initialized, update ksit
    if (this.activeUsersChart) {
      this.activeUsersChart.data.datasets[0].data = [
        this.activeCount,
        this.terminationCount,
        this.expiredCount,
      ];
      this.activeUsersChart.update();
    }
  }

  /**
   * Fetches all member data from the API and updates the component state accordingly.
   *
   * This method initiates an asynchronous API call to retrieve all gym member information
   * and manages the loading state throughout the request lifecycle. It handles both
   * successful responses and error scenarios, updating the component's member details
   * signal and displaying appropriate user feedback via toaster notifications.
   *
   * @returns {void} This method does not return a value
   */
  private getAllMembersData(): void {
    this.isLoading.set(true);

    this.membersApiService.GetAllMembersAsync().subscribe({
      next: (response: ResponseDto) => {
        if (response && response?.isSuccess) {
          this.memberDetails.set(response.responseData);
          this.updateChartDataFromMembers(response.responseData);
        } else {
          this.toasterService.showError(response?.responseData);
        }
      },
      error: (error: any) => {
        this.isLoading.set(false);
        console.error(error);
        this.toasterService.showError(error?.message);
      },
      complete: () => {
        this.isLoading.set(false);
      },
    });
  }
}
