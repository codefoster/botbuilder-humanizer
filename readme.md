# Introduction

This Microsoft Bot Builder v4 middleware component will give your bot some human feel by randomly injecting misspellings, letter transpositions, and even messages to the wrong recipient - complete with followup corrections!

# Getting Started

To get started, install `botbuilder-humanizer` in your bot project.

```bash
npm install botbuilder-humanizer
```

And then `import` and `.use` Humanizer. Here's a simple echo bot.

```js
import { Bot } from 'botbuilder';
import { ConsoleAdapter } from 'botbuilder-node';
import { Humanizer } from "botbuilder-humanizer";

const bot = new Bot(new ConsoleAdapter().listen());

bot
    .use(new Humanizer({ 
        misspelling: .25,
        wrongPerson: .001,
        transpositions: .008,
        typingSpeed: 120
     }))
    .onReceive(context => {
        context.reply(context.request.text);
    });
```

The numeric values you pass in represent the probability that anomolies of a given type will occur. For instance, `.25` for `misspelling` means that [commonly misspelled words](src/misspellings.ts) will be misspelled 25% of the time.