export class CurrencyDirective {
    constructor(){
        this.restrict = 'A';
        this.require = 'ngModel';
    }

    // compile(elem){
    //     console.log('app.common.currency: compile', elem);
    //     if(elem.hasClass('money') === false){
    //         elem.addClass('money');
    //     }

        
    // }

    link(scope, elem, attrs, ngModel){
        elem.addClass('money');
        elem.addClass('text-right');

        ngModel.$formatters.push((number) => {
            var locale = 'en-US';
            if(navigator){
                if(navigator.languages){
                    locale = navigator.languages[0];
                }
                else{
                    locale = navigator.language;
                }
            }
            var viewValue = number.toLocaleString(locale, { style : 'currency', currency : 'USD' });       
            ngModel.$setViewValue(viewValue);
            return number;
        })
    }
}