# Commentary
- For this task, I used Suno to generate AI music that I then resampled. I initially generated a bass line by entering the genre and selecting the "instrumental only" option, as I did
not want any AI lyrics. After the bass was created, I trimmed the sample and uploaded it to Suno as a "reference track." I entered new prompts, such as BPM, style, and requested a beat
switch. I wanted to experiment as much as possible, giving myself the opportunity to choose from different tracks to see how far I could push Suno to generate decent "music."
- Once I was happy with the outcomes, I selected two different tracks containing the same bass line and imported them into FMOD. Initially, I intended to use three different tracks but
faced problems with a parameter I created later in the project. Since I had already spent a significant amount of time on this, I decided to cut it down to two. With the tracks in FMOD,
I started thinking about how to make the audio sound interesting.
- From previous projects, I remembered that I could create a parameter that changes the volume when a certain point is reached or if linked with a script in Unity. However, I didn't want
to stop there. I wanted the parameter to be linked with a "transition condition" as well, so that the swap between the first and second samples would occur even more smoothly. Since this
was quite ambitious, I got straight to work.
- The parameter was created and working perfectly, and the transitions were happening as expected, so I built the FMOD project to link it in Unity. I opened Unity, created a new game object,
attached the "FMOD Event Emitter," and set the path to the FMOD project. So far, so good. However, when I pressed play, the music came through the speakers, but changing the parameter for the
transition had no effect. I was confused because it worked in FMOD and in Unity's "preview" window, just not in the actual "Play" mode.
- I was really confused and spent several hours trying to identify the issue. Eventually, I decided to write a script to control the parameter; every ten seconds, it would swap between the
samples once a certain point was reached. After attaching the script to the game object, the transition was very messy and did not work as intended. I went back into FMOD, unlinked the
parameter from the "transition condition," and hoped I would still get a nice fade in/out.
- had to change the script and add a "sustain" type condition because the songs would change every 10 seconds, but the "new part" would only last for a second before reverting to the initial
sample. After I fixed this, everything worked as intended.
- I can say with certainty that the use of Suno (AI Music) enhanced my production workflow because I did not have to spend hours producing the music myself before getting on with the
technical task. The only downside is that a listener can tell by ear that the music is not "organic."
