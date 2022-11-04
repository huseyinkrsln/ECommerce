import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

export class BaseComponent {
  constructor(private spinner: NgxSpinnerService) {}

  showSpinner(spinnerNameType:Spinnertype) {
    this.spinner.show(spinnerNameType);

    setTimeout(() => {
      this.hideSpinner(spinnerNameType)
    }, 2000);
  }
  hideSpinner(spinnerNameType: Spinnertype){
    this.spinner.hide(spinnerNameType);
  }
}

export enum Spinnertype {
  BallAtom = 's3',
  ballScaleMultiple = 's1',
  ballElasticDots = 's2',
}
