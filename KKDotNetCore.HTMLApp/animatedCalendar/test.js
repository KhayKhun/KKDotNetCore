const OBJ = {
  name: "john",
  run: function (a,b) {
    console.log("running " + a +b);
  },
};
const functionString = OBJ.run.toString();
OBJ.run = functionString;
const str = JSON.stringify( OBJ);
localStorage.setItem("man", str);
let man = JSON.parse(localStorage.getItem("man"));
console.log(man.run);

man.run = new Function('return '+ man.run)();
man.run("man","mm")