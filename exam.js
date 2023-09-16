const euclid = (a,b) => {
    var check = a%b 
    while(check != 0 ) {

            a = b
            b = check 
            check = a%b
       
    }
    return b 
}
var a = 710
var b = 310
console.log(euclid(b,a))