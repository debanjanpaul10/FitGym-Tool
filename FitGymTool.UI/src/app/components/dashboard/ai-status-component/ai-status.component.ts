import { Component, OnInit, OnDestroy } from '@angular/core';
import { Ripple } from 'primeng/ripple';

@Component({
  selector: 'app-ai-status-component',
  imports: [Ripple],
  templateUrl: './ai-status.component.html',
  styleUrl: './ai-status.component.scss',
})
export class AiStatusComponent implements OnInit, OnDestroy {
  protected statusText: string = '';
  protected chartPath: string = '';
  protected isOnline: boolean = false;
  private intervalId: any;

  ngOnInit() {
    this.generateChartPath();
    this.startStatusUpdates();
    this.statusText = this.isOnline ? 'Active' : 'Offline';
  }

  ngOnDestroy() {
    if (this.intervalId) {
      clearInterval(this.intervalId);
    }
  }

  private generateChartPath(): void {
    let points;

    if (this.isOnline) {
      points = [
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
    } else {
      points = [
        { x: 0, y: 10 },
        { x: 15, y: 8 },
        { x: 30, y: 5 },
        { x: 45, y: 12 },
        { x: 60, y: 25 },
        { x: 75, y: 32 },
        { x: 90, y: 22 },
        { x: 105, y: 28 },
        { x: 120, y: 30 },
      ];
    }

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
      this.statusText = this.isOnline ? 'Active' : 'Offline';
      this.generateChartWithVariation();
    }, 5000);
  }

  private generateChartWithVariation(): void {
    let basePoints;

    if (this.isOnline) {
      basePoints = [
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
    } else {
      basePoints = [
        { x: 0, y: 10 },
        { x: 15, y: 8 },
        { x: 30, y: 5 },
        { x: 45, y: 12 },
        { x: 60, y: 25 },
        { x: 75, y: 32 },
        { x: 90, y: 22 },
        { x: 105, y: 28 },
        { x: 120, y: 30 },
      ];
    }

    const points = basePoints.map((point) => ({
      x: point.x,
      y: point.y + (Math.random() - 0.5) * 3,
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
