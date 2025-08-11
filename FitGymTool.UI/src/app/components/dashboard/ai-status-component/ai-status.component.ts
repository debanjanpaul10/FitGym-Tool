import { Component, OnInit, OnDestroy } from '@angular/core';
import { Ripple } from 'primeng/ripple';

@Component({
  selector: 'app-ai-status-component',
  imports: [Ripple],
  templateUrl: './ai-status.component.html',
  styleUrl: './ai-status.component.scss',
})
export class AiStatusComponent implements OnInit, OnDestroy {
  protected statusText: string = 'Active';
  protected chartPath: string = '';
  private intervalId: any;

  ngOnInit() {
    this.generateChartPath();
    this.startStatusUpdates();
  }

  ngOnDestroy() {
    if (this.intervalId) {
      clearInterval(this.intervalId);
    }
  }

  private generateChartPath(): void {
    const points = [
      { x: 0, y: 30 },
      { x: 15, y: 32 },
      { x: 30, y: 35 },
      { x: 45, y: 28 },
      { x: 60, y: 15 },
      { x: 75, y: 8 },
      { x: 90, y: 18 },
      { x: 105, y: 12 },
      { x: 120, y: 10 },
    ];

    let path = `M ${points[0].x} ${points[0].y}`;

    for (let i = 1; i < points.length; i++) {
      const prevPoint = points[i - 1];
      const currentPoint = points[i];
      const controlPoint1 = {
        x: prevPoint.x + (currentPoint.x - prevPoint.x) * 0.4,
        y: prevPoint.y,
      };
      const controlPoint2 = {
        x: currentPoint.x - (currentPoint.x - prevPoint.x) * 0.4,
        y: currentPoint.y,
      };

      path += ` C ${controlPoint1.x} ${controlPoint1.y}, ${controlPoint2.x} ${controlPoint2.y}, ${currentPoint.x} ${currentPoint.y}`;
    }

    this.chartPath = path;
  }

  private startStatusUpdates(): void {
    this.intervalId = setInterval(() => {
      this.generateChartWithVariation();
    }, 3000);
  }

  private generateChartWithVariation(): void {
    const basePoints = [
      { x: 0, y: 30 },
      { x: 15, y: 32 },
      { x: 30, y: 35 },
      { x: 45, y: 28 },
      { x: 60, y: 15 },
      { x: 75, y: 8 },
      { x: 90, y: 18 },
      { x: 105, y: 12 },
      { x: 120, y: 10 },
    ];

    const points = basePoints.map((point) => ({
      x: point.x,
      y: point.y + (Math.random() - 0.5) * 4,
    }));

    let path = `M ${points[0].x} ${points[0].y}`;

    for (let i = 1; i < points.length; i++) {
      const prevPoint = points[i - 1];
      const currentPoint = points[i];
      const controlPoint1 = {
        x: prevPoint.x + (currentPoint.x - prevPoint.x) * 0.4,
        y: prevPoint.y,
      };
      const controlPoint2 = {
        x: currentPoint.x - (currentPoint.x - prevPoint.x) * 0.4,
        y: currentPoint.y,
      };

      path += ` C ${controlPoint1.x} ${controlPoint1.y}, ${controlPoint2.x} ${controlPoint2.y}, ${currentPoint.x} ${currentPoint.y}`;
    }

    this.chartPath = path;
  }
}
