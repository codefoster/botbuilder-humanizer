"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : new P(function (resolve) { resolve(result.value); }).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
Object.defineProperty(exports, "__esModule", { value: true });
const misspellings_1 = require("./misspellings");
class Humanizer {
    constructor(options) {
        this.misspelling = .25;
        this.wrongPerson = .001;
        this.transpositions = .008;
        this.typingSpeed = 300;
        if (options.misspelling)
            this.misspelling = options.misspelling;
        if (options.wrongPerson)
            this.wrongPerson = options.wrongPerson;
        if (options.transpositions)
            this.transpositions = options.transpositions;
        if (options.typingSpeed)
            this.typingSpeed = options.typingSpeed;
    }
    receiveActivity(context, next) {
        return __awaiter(this, void 0, void 0, function* () {
            yield next();
            context.responses.forEach(response => {
                let delay = response.text.split(' ').length / (this.typingSpeed / 60 / 1000);
                //transpositions
                if (Math.random() < this.transpositions) {
                    let text = Array.from(response.text);
                    let position = this.random(0, text.length - 2);
                    let aux = text[position + 1];
                    text[position + 1] = text[position];
                    text[position] = aux;
                    response.text = text.join('');
                }
                //wrongPerson
                if (Math.random() < this.wrongPerson) {
                    let statements = [
                        "not a chance",
                        "thursday",
                        "I said 7!!",
                        "thbbbbpt!"
                    ];
                    this.typingDelay(context, delay);
                    context.reply(statements[this.random(0, statements.length - 1)]);
                    this.typingDelay(context, delay);
                    context.reply("sorry... that last message was meant for someone else");
                }
                //misspelling
                response.text.split(' ').forEach(word => {
                    if (misspellings_1.default.hasOwnProperty(word) && Math.random() < this.misspelling) {
                        response.text = response.text.replace(word, misspellings_1.default[word]);
                        this.typingDelay(context, delay);
                        context.reply(`${word}*`);
                    }
                });
            });
        });
    }
    typingDelay(context, delay) {
        return context.showTyping().delay(delay);
    }
    random(a, b) {
        return Math.round(b + (Math.random() * (a - b)));
    }
}
exports.Humanizer = Humanizer;
//# sourceMappingURL=index.js.map