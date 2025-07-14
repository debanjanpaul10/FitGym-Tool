import { Component, signal, WritableSignal } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';

@Component({
  selector: 'app-add-user-component',
  imports: [DialogModule, ButtonModule],
  templateUrl: './add-user.component.html',
  styleUrl: './add-user.component.scss',
})
export class AddUserComponent {
  public visible: WritableSignal<boolean> = signal(false);
}
