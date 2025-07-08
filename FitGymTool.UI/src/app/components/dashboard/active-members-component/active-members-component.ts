import { Component } from '@angular/core';
import { CardModule } from 'primeng/card';

@Component({
  selector: 'app-active-members-component',
  imports: [CardModule],
  templateUrl: './active-members-component.html',
  styleUrl: './active-members-component.scss',
})
export class ActiveMembersComponent {}
