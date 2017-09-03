# LedySync

## About
This additional program is used to sync several instances of Ledybot together to prevent them from trying to trade the same person at the same time and to have a shared list of people that already got traded. To be used with https://github.com/olliz0r/Ledybot

If you are rich and like what you see, feel free to throw me a donation at https://www.paypal.me/olliz0r !

## Usage
1. Start LedySync
2. Type in a free port to use (if you don't know what this is, just leave the 3000)
3. Fill the timeout field with a number of seconds before each FC can be traded again
4. (Optional) Fill the BanList with FC that should never be traded. The format is "432156781234", so the FC without any dashes, seperated by a newline.
5. Press Start
6. Start one or several instances of Ledybot
7. In the settings tab check the "Use LedySync" checkbox
8. Type in the IP-address of the computer that runs LedySync and the port set in 2. (If you don't know your ip and you are running both programs on the same computer, put "127.0.0.1")
9. (Optional) Set a console name for this Ledybot instance, this will help you see which Ledybot traded which person
10. Set all other settings in Ledybot as you want them
11. Connect and press start

## How it works
Once set up each Ledybot instance will establish a connection to LedySync and ask before trading to a certain FC. LedySync will then reply by either:
* Telling Ledybot to put the person on the internal ban list if this FC is on the LedySync ban list
* Telling Ledybot to skip this person if this FC has been traded recently
* Telling Ledybot to trade this person if none of the above apply

## Hints and information
- LedySync will override the blacklist feature of LedyBot, however, the banlist feature still applies. This is so that Ledybot can skip FCs without having to ask LedySync for permission
- When setting the timeout between trades, set at least 30-60 seconds, even if you want people to request an unlimited amount of pokemon. This is essential to prevent two Ledybots trying to trade the same person at the same time
- If you want people to only be able to get a single pokemon, set the time to something reasonably high (86400 for a day, 2073600 for a week), don't set the value too high or LedySync won't be able to read the number as integer (we are talking about a value in the 2 billions here)
- Only the first 12 digits of the lines in the Banlist will be read as FC and banned, you can also use a line or the space behind a FC to write down why this person was banned
- The Banlist saves itself to a .txt file next to LedySync 

## Todo
- [ ] Sync giveaway details over LedySync
- [ ] Reddit support (currently disabled when using LedySync)
- [ ] Documentation and some error handling
- [ ] Live editing of the blacklist
- [ ] Stat tracking?
- [ ] Priority list? There's two stages to this, either people don't get added to the blacklist or actually make them trade first if they show up, this would have to be added to Ledybot itself though
