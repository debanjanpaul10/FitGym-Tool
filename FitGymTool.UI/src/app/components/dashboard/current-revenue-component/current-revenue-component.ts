import {
  Component,
  AfterViewInit,
  ViewChild,
  ElementRef,
  OnDestroy,
} from '@angular/core';
import Chart from 'chart.js/auto';

import { ChartConstants } from '@shared/application.constants';

/**
 * CurrentRevenueComponent displays a horizontal bar chart representing the current revenue status.
 * It uses Chart.js to visualize paid, unpaid, and cancelled subscription fees for the current month.
 * The chart is styled to match the application's theme and uses constants for labels and colors.
 */
@Component({
  selector: 'app-current-revenue-component',
  imports: [],
  templateUrl: './current-revenue-component.html',
  styleUrl: './current-revenue-component.scss',
})
export class CurrentRevenueComponent implements AfterViewInit, OnDestroy {
  /** Reference to the chart canvas element in the template. */
  @ViewChild('revenueChartCanvas', { static: false })
  revenueChartCanvas!: ElementRef<HTMLCanvasElement>;

  /** Provides access to chart-related constants for labels and subheader. */
  public chartConstants = ChartConstants.RevenueChartConstants;

  /** Holds the Chart.js instance for the revenue chart. */
  private revenueChart: Chart | null = null;

  /**
   * Lifecycle hook that is called after Angular has fully initialized the component's view.
   * Initializes the revenue chart.
   */
  ngAfterViewInit(): void {
    this.createChart();
  }

  /**
   * Creates and configures the Chart.js horizontal bar chart for revenue data.
   * Uses application constants for labels and colors, and customizes the chart's appearance to match the app theme.
   */
  private createChart(): void {
    if (this.revenueChartCanvas) {
      const customLabels = [
        this.chartConstants.Labels.Paid.legend,
        this.chartConstants.Labels.NotPaid.legend,
        this.chartConstants.Labels.SubCancelled.legend,
      ];
      this.revenueChart = new Chart(this.revenueChartCanvas.nativeElement, {
        type: 'bar',
        data: {
          labels: [
            this.chartConstants.Labels.Paid.yAxis,
            this.chartConstants.Labels.NotPaid.yAxis,
            this.chartConstants.Labels.SubCancelled.yAxis,
          ],
          datasets: [
            {
              label: this.chartConstants.SubHeader,
              data: [25000, 20000, 1200],
              backgroundColor: ['#7CFC98', '#FF7C7C', '#D3D3D3'],
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
                /**
                 * Customizes the tooltip label for each bar to show a descriptive label and value.
                 * @param context Chart.js tooltip context
                 * @returns {string} The formatted tooltip label
                 */
                label: function (context: any): string {
                  const labelIndex = context.dataIndex;
                  const value = context.parsed.x || context.parsed.y;
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

  ngOnDestroy(): void {
    this.revenueChart?.destroy();
  }
}
