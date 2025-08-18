import {
  Component,
  inject,
  Input,
  OnInit,
  signal,
  WritableSignal,
} from '@angular/core';
import { NgClass } from '@angular/common';
import { Ripple } from 'primeng/ripple';
import { Button } from 'primeng/button';

import { LoaderService } from '@core/services/loader.service';
import { ToasterService } from '@core/services/toaster.service';
import { AIFeaturesDTO } from '@models/DTO/ai-features-dto.model';
import { ResponseDto } from '@models/DTO/response-dto.model';
import { CommonApiService } from '@services/common-api.service';
import { AiFeaturesListComponent } from '../ai-features-list-component/ai-features-list.component';
import { DialogPopupService } from '@core/services/dialog-popup.service';
import { CommonApplicationConstants } from '@shared/application.constants';
import { MasterMappingDataDto } from '@models/DTO/Mapping/master-mapping-dto.model';

@Component({
  selector: 'app-ai-features-component',
  imports: [Ripple, NgClass, AiFeaturesListComponent, Button],
  templateUrl: './ai-features.component.html',
  styleUrl: './ai-features.component.scss',
})
export class AiFeaturesComponent implements OnInit {
  @Input() mappingsMasterData: MasterMappingDataDto =
    new MasterMappingDataDto();

  protected activeAiFeatures: WritableSignal<AIFeaturesDTO[]> = signal([]);
  protected headersConstants = CommonApplicationConstants.HeaderConstants;

  private readonly _commonApiService: CommonApiService =
    inject(CommonApiService);
  private readonly _toasterService: ToasterService = inject(ToasterService);
  private readonly _loaderService: LoaderService = inject(LoaderService);
  private readonly _dialogPopupService: DialogPopupService =
    inject(DialogPopupService);

  ngOnInit(): void {
    this.getActiveAiFeatures();
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

  protected isServiceActive(statusId: number): boolean {
    const statusName = this.getServiceStatusName(statusId);
    return statusName.toLowerCase() === 'active';
  }

  private getActiveAiFeatures(): void {
    this._loaderService.loadingOn();

    this._commonApiService.GetActiveAIFeaturesAsync().subscribe({
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
