import {
  Component,
  inject,
  Input,
  OnInit,
  Output,
  signal,
  WritableSignal,
  effect,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { DialogModule } from 'primeng/dialog';
import { Button } from 'primeng/button';
import { TableModule } from 'primeng/table';
import { Ripple } from 'primeng/ripple';
import { TagModule } from 'primeng/tag';

import { DialogPopupService } from '@core/services/dialog-popup.service';
import { AIFeaturesDTO } from '@models/DTO/ai-features-dto.model';
import { CommonApplicationConstants } from '@shared/application.constants';
import { AIServiceStatusMappingDTO } from '@models/DTO/Mapping/ai-service-status-mapping-dto.model';
import { Column } from '@models/interfaces/column.interface';
import { Utilities } from '@core/helpers/utilities-helper';

@Component({
  selector: 'app-ai-features-list-component',
  imports: [DialogModule, Button, TableModule, CommonModule, Ripple, TagModule],
  templateUrl: './ai-features-list.component.html',
  styleUrl: './ai-features-list.component.scss',
})
export class AiFeaturesListComponent implements OnInit {
  @Input() aiFeaturesList: WritableSignal<AIFeaturesDTO[]> = signal([]);
  @Input() aiServiceStatusMapping: WritableSignal<AIServiceStatusMappingDTO[]> =
    signal([]);
  @Output() protected visible: WritableSignal<boolean> = signal(false);

  protected headersConstants = CommonApplicationConstants.HeaderConstants;
  protected columnHeaders: Column[] = [];
  protected aiServicesWithStatus: any[] = [];
  protected getStatusChipClass = Utilities.getStatusChipClass;

  private readonly _dialogPopupService: DialogPopupService =
    inject(DialogPopupService);

  constructor() {
    this.visible = this._dialogPopupService.isAiFeaturesDialogOpen;
    this.columnHeaders = [
      { field: 'serviceName', header: 'Service Name' },
      { field: 'serviceDescription', header: 'Service Description' },
      { field: 'serviceStatus', header: 'Service Status' },
    ];

    // Watch for changes in inputs and remap when they change
    effect(() => {
      this.mapAiServicesWithStatus();
    });
  }

  ngOnInit(): void {
    this.mapAiServicesWithStatus();
  }

  /**
   * Handles the cancel click event.
   */
  protected onCancel(): void {
    this.visible.set(false);
  }

  /**
   * Maps AI features with their corresponding status names from the status mapping
   */
  private mapAiServicesWithStatus(): void {
    const statusMapping = this.aiServiceStatusMapping();
    this.aiServicesWithStatus = this.aiFeaturesList().map((service) => ({
      ...service,
      serviceStatus: this.getStatusName(service.serviceStatusId, statusMapping),
    }));
  }

  /**
   * Gets the status name from the mapping based on status ID
   */
  private getStatusName(
    statusId: number,
    statusMapping: AIServiceStatusMappingDTO[]
  ): string {
    const status = statusMapping.find((mapping) => mapping.id === statusId);
    return status ? status.statusName : 'Unknown';
  }
}
