import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'rr-top-message',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './top-message.component.html',
  styleUrls: ['./top-message.component.scss'],
})
export class TopMessageComponent  {
 
  @Input() message?: string; 

}
