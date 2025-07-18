import { Component } from '@angular/core';

/**
 * Footer component that displays application footer information.
 * This component provides a reusable footer section for the application,
 * typically containing copyright information, links, and other footer content.
 * The component automatically displays the current year for copyright purposes.
 */
@Component({
  selector: 'app-footer-component',
  imports: [],
  templateUrl: './footer.component.html',
  styleUrl: './footer.component.scss',
})
export class FooterComponent {
  public currentYear: number = new Date().getFullYear();
}
