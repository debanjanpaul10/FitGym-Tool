import {
  Component,
  inject,
  OnInit,
  signal,
  WritableSignal,
} from '@angular/core';
import { Ripple } from 'primeng/ripple';

import { LoaderService } from '@core/services/loader.service';
import { ToasterService } from '@core/services/toaster.service';
import { AIFeaturesDTO } from '@models/DTO/ai-features-dto.model';
import { ResponseDto } from '@models/DTO/response-dto.model';
import { CommonApiService } from '@services/common-api.service';

@Component({
  selector: 'app-ai-features-component',
  imports: [Ripple],
  templateUrl: './ai-features.component.html',
  styleUrl: './ai-features.component.scss',
})
export class AiFeaturesComponent implements OnInit {
  protected activeAiFeatures: WritableSignal<AIFeaturesDTO[]> = signal([]);

  private readonly _commonApiService: CommonApiService =
    inject(CommonApiService);
  private readonly _toasterService: ToasterService = inject(ToasterService);
  private readonly _loaderService: LoaderService = inject(LoaderService);

  ngOnInit(): void {
    this.getActiveAiFeatures();
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
