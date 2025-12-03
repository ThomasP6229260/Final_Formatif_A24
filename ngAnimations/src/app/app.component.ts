import { transition, trigger, useAnimation } from '@angular/animations';
import { Component } from '@angular/core';
import { bounce, shakeX, tada } from 'ng-animate';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css'],
    standalone: true,
    animations: [
  trigger("death", [
    transition(
      ":increment",
      useAnimation(shakeX, { params: { timing: 2.0 } })
    ),
  ]),

  trigger("green", [
    transition(
      ":increment",
      useAnimation(bounce, { params: { timing: 4.0 } })
    ),
  ]),
   trigger("blue", [
    transition(
      ":increment",
      useAnimation(tada, { params: { timing: 3.0 } })
    ),
  ]),
  



],
})
export class AppComponent {
    slimeIsPresent = false;
  DEATH_DURATION_SECONDS : number = 0.5;
  ng_death: number = 0;
  ng_green : number = 0;
  ng_blue : number = 0;

   ng_death3  : number = 0;
   ng_flip  : number = 0;

   ng_bounce : number = 0;
   css_rotateTop : boolean = false;
   css_rotateCenter : boolean = false;
  title = 'ngAnimations';

  constructor() {
  }

  death() {
     this.ng_death++;

      setTimeout(() => {
    // Après 1 seconde
    this.green();
  }, 1300);
     
  }

   green() {
     this.ng_green++;

      setTimeout(() => {
    // Après 1 seconde
    this.blue();
  }, 3000);
  }

  blue() {
    this.ng_blue++;
  }



    death2() {
     this.ng_death++;

      setTimeout(() => {
    // Après 1 seconde
    this.green2();
  }, 1300);
     
  }

   green2() {
     this.ng_green++;

      setTimeout(() => {
    // Après 1 seconde
    this.blue2();
  }, 3000);
  }

  blue2() {
    this.ng_blue++;
 setTimeout(() => {
    // Après 1 seconde
    this.death2();
  }, 3000);
   // this.death2();
    
  }

  death3() {
    
  }
}
