import { asyncScheduler, from, fromEvent, interval, Observable, of, scheduled, take, timer } from 'rxjs';
import { Component, inject, PLATFORM_ID, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NgxSpinnerComponent } from "ngx-spinner";
import { log } from 'console';
import { subscribe, unsubscribe } from 'diagnostics_channel';
import { resolve } from 'path';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, NgxSpinnerComponent],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
}


