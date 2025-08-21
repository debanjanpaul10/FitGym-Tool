import {
  Component,
  inject,
  Input,
  OnInit,
  signal,
  WritableSignal,
} from '@angular/core';
import { Ripple } from 'primeng/ripple';
import { Button } from 'primeng/button';
import { TagModule } from 'primeng/tag';

import { LoaderService } from '@core/services/loader.service';
import { ToasterService } from '@core/services/toaster.service';
import { AIFeaturesDTO } from '@models/DTO/ai-features-dto.model';
import { ResponseDto } from '@models/DTO/response-dto.model';
import { AiFeaturesListComponent } from '../ai-features-list-component/ai-features-list.component';
import { DialogPopupService } from '@core/services/dialog-popup.service';
import { CommonApplicationConstants } from '@shared/application.constants';
import { MasterMappingDataDto } from '@models/DTO/Mapping/master-mapping-dto.model';
import { AIServiceStatusMappingDTO } from '@models/DTO/Mapping/ai-service-status-mapping-dto.model';
import { Utilities } from '@core/helpers/utilities-helper';
import { AiApiService } from '@services/ai-services-api.service';

@Component({
  selector: 'app-ai-features-component',
  imports: [Ripple, AiFeaturesListComponent, Button, TagModule],
  templateUrl: './ai-features.component.html',
  styleUrl: './ai-features.component.scss',
})
export class AiFeaturesComponent implements OnInit {
  @Input() mappingsMasterData: MasterMappingDataDto =
    new MasterMappingDataDto();

  protected activeAiFeatures: WritableSignal<AIFeaturesDTO[]> = signal([]);
  protected aiServiceStatusMapping: WritableSignal<
    AIServiceStatusMappingDTO[]
  > = signal([]);
  protected headersConstants = CommonApplicationConstants.HeaderConstants;
  protected getStatusChipClass = Utilities.getStatusChipClass;

  private readonly _toasterService: ToasterService = inject(ToasterService);
  private readonly _loaderService: LoaderService = inject(LoaderService);
  private readonly _dialogPopupService: DialogPopupService =
    inject(DialogPopupService);
  private readonly _aiApiService: AiApiService = inject(AiApiService);

  ngOnInit(): void {
    this.getActiveAiFeatures();
    this.aiServiceStatusMapping.set(
      this.mappingsMasterData.aiServiceStatusMappings
    );
  }

  protected openAIFeaturesList(): void {
    this._dialogPopupService.openAiFeaturesDialog();
  }

  protected getServiceStatusName(statusId: number): string {
    const statusMapping = this.mappingsMasterData.aiServiceStatusMappings.find(
      (mapping) => mapping.id === statusId
    );
    return statusMapping?.statusName || 'Unknown';
  }

  protected getChipColour(statusId: number) {
    const statusName = this.getServiceStatusName(statusId).toLocaleLowerCase();
    return this.getStatusChipClass(statusName);
  }

  private getActiveAiFeatures(): void {
    this._loaderService.loadingOn();

    this._aiApiService.GetActiveAIFeaturesAsync().subscribe({
      next: (response: ResponseDto) => {
        if (response?.isSuccess && response?.responseData) {
          this.activeAiFeatures.set(response?.responseData);
        } else {
          this._toasterService.showError(response?.responseData);
          console.error(response?.responseData);
        }
      },
      error: (err: Error) => {
        this._loaderService.loadingOff();
        this._toasterService.showError(err?.message);
        console.error(err);
      },
      complete: () => {
        this._loaderService.loadingOff();
      },
    });
  }
}
