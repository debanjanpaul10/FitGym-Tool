import { Component } from '@angular/core';
import { ToastModule } from 'primeng/toast';

/**
 * Toaster component that displays toast notifications to users.
 * This component provides a container for displaying success, error, warning, and info messages
 * throughout the application. It integrates with PrimeNG's Toast module to render notifications
 * and is typically controlled by the ToasterService for centralized message management.
 */
@Component({
  selector: 'app-toaster-component',
  imports: [ToastModule],
  templateUrl: './toaster.component.html',
  styleUrl: './toaster.component.scss',
})
export class ToasterComponent {}
