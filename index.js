const readline = require("readline");
const rl = readline.createInterface(process.stdin, process.stdout);
const { createHmac } = require("crypto");

console.log();
let hashKey = "";
function hashPassword() {
  rl.setPrompt("Salt\n> ");
  rl.prompt();
  rl.on("line", (salt) => {
    hashKey = salt;
    rl.setPrompt("Hash This\n> ");
    rl.prompt();
    rl.on("line", (password) => {
      if (typeof password !== "string") {
        console.log("Failed, password must be of type string");
        hashPassword();
        return;
      }
      console.log(createHmac("SHA256", hashKey).update(password).digest("hex").toString("utf-8"));
      rl.close();
    });
  });
}
hashPassword();
