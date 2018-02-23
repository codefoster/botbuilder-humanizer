import { Middleware } from "botbuilder";
import misspellings from "./misspellings";

export class Humanizer implements Middleware {
    misspelling: number = .25;
    wrongPerson: number = .001;
    transpositions: number = .008;
    typingSpeed: number = 300;

    constructor(options: HumanizerOptions) {
        if (options.misspelling) this.misspelling = options.misspelling;
        if (options.wrongPerson) this.wrongPerson = options.wrongPerson;
        if (options.transpositions) this.transpositions = options.transpositions;
        if (options.typingSpeed) this.typingSpeed = options.typingSpeed;
    }

    public async receiveActivity(context: BotContext, next: () => Promise<void>): Promise<void> {
        await next();

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
                context.reply(statements[this.random(0, statements.length - 1)])
                this.typingDelay(context, delay);
                context.reply("sorry... that last message was meant for someone else");
            }

            //misspelling
            response.text.split(' ').forEach(word => {
                if (misspellings.hasOwnProperty(word) && Math.random() < this.misspelling) {
                    response.text = response.text.replace(word, misspellings[word]);
                    this.typingDelay(context, delay);
                    context.reply(`${word}*`);
                }
            })

        })
    }

    typingDelay(context, delay) {
        return context.showTyping().delay(delay);
    }

    random(a, b) {
        return Math.round(b + (Math.random() * (a - b)))
    }
}

interface HumanizerOptions {
    misspelling: number;
    wrongPerson: number;
    transpositions: number;
    typingSpeed: number
}