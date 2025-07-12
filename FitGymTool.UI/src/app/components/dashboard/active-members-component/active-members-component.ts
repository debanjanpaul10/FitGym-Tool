import {
  AfterViewInit,
  Component,
  ElementRef,
  OnDestroy,
  ViewChild,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChartConstants } from '@shared/application.constants';
import Chart from 'chart.js/auto';

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
  imports: [CommonModule],
  templateUrl: './active-members-component.html',
  styleUrl: './active-members-component.scss',
})
export class ActiveMembersComponent implements AfterViewInit, OnDestroy {
  /**
   * Reference to the canvas element where the chart will be rendered.
   * Used to get the native HTML canvas element for Chart.js initialization.
   */
  @ViewChild('activeUsersChartCanvas', { static: false })
  activeUsersChartCanvas!: ElementRef<HTMLCanvasElement>;

  /**
   * Chart configuration constants imported from the shared constants file.
   * Contains chart-specific settings and labels.
   */
  public chartConstants = ChartConstants.ActiveUsersChartConstants;

  /**
   * Count of active members displayed next to the chart.
   */
  public activeCount: number = 50;

  /**
   * Count of members on termination displayed next to the chart.
   */
  public terminationCount: number = 2;

  /**
   * Instance of the Chart.js chart object.
   * Holds the reference to the created chart for proper cleanup and management.
   */
  private activeUsersChart: Chart | null = null;

  /**
   * Lifecycle hook that is called after the component's view has been initialized.
   *
   * This method is called after Angular has fully initialized the component's view,
   * including all child views and the view query results. It's the ideal place to
   * initialize the chart since the canvas element is now available in the DOM.
   *
   * @implements AfterViewInit
   */
  ngAfterViewInit(): void {
    // Use setTimeout to ensure the canvas is fully rendered
    setTimeout(() => {
      this.createChart();
    }, 0);
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
   *
   * @private
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
            ],
            datasets: [
              {
                label: this.chartConstants.SubHeader,
                data: [this.activeCount, this.terminationCount],
                backgroundColor: ['#7CFC98', '#FF7C7C'],
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
   * Lifecycle hook that is called when the component is about to be destroyed.
   *
   * This method ensures proper cleanup of the Chart.js instance to prevent memory leaks.
   * It calls the destroy() method on the chart instance, which removes event listeners,
   * clears the canvas, and frees up memory resources associated with the chart.
   *
   * @implements OnDestroy
   */
  ngOnDestroy(): void {
    this.activeUsersChart?.destroy();
  }
}
