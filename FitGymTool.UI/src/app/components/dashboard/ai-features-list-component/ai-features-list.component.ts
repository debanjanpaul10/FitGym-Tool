import {
  Component,
  inject,
  Input,
  Output,
  signal,
  WritableSignal,
} from '@angular/core';
import { DialogModule } from 'primeng/dialog';
import { Button } from 'primeng/button';

import { DialogPopupService } from '@core/services/dialog-popup.service';
import { AIFeaturesDTO } from '@models/DTO/ai-features-dto.model';
import { CommonApplicationConstants } from '@shared/application.constants';

@Component({
  selector: 'app-ai-features-list-component',
  imports: [DialogModule, Button],
  templateUrl: './ai-features-list.component.html',
  styleUrl: './ai-features-list.component.scss',
})
export class AiFeaturesListComponent {
  @Input() aiFeaturesList: AIFeaturesDTO[] = [];
  @Output() protected visible: WritableSignal<boolean> = signal(false);

  protected headersConstants = CommonApplicationConstants.HeaderConstants;

  private readonly _dialogPopupService: DialogPopupService = inject(DialogPopupService);

  constructor() {
    this.visible = this._dialogPopupService.isAiFeaturesDialogOpen;
  }

  protected onCancel(): void {
    this.visible.set(false);
  }
}
