# Topic

## Using Unity to Create 3D Games with AI Integration

This project is NOT a game, it's a PROTOTYPE (PoC) to showcase where/how AI can be used.

AI is, for example, LLM, Math (perlin noise), complicated algorythms/scripts, strict rules, etc...

Here I showcase:

- Landspaces generation
- NPC bot vs NPC ai (human made responces vs llm responses)
- WIP: enemies (human vs neural network)

# Dependencies

The project uses a local LLM for some NPCs to work.

You need to download Ollama (the project uses small language model: **phi3**) for some neural network AI npcs to work.

https://ollama.com

After that you need to run Ollama, the project uses **phi3 model**:

If you use Linux, run 

```
ollama serve

ollama run phi3
```

The Ollama works when you can visit your localhost link (or in cloud, I used local SLM) in your browser. It should work by this link if you downloaded and ran Ollama:

http://127.0.0.1:11434/

(or any other link/app/whatever where you can check if it works)

# Known issues

**States aren't synchronized at first**
When you want to interact with an NPC at first time, it will show up a blank dialogue text. This is where the AI (input field) text goes to. For example, if you got to a NPC with yellow hat (who is just a bot), it will be blank, then you go to the green one -- it has got its dialogue. Yes, it will show up your prompt, but the response will get the yellow hat NPC. 

**In Unity: you can't move objects in hierarchy**
Moving can be fixed by cutting needed items and pasting them. That happens at least for me on Gentoo Linux, while a completely new project without new added objects can be moved as usually.

# Movement
Only needed to showcase the landscapes, enemy's AI and NPCs

WASD - movement

SPACE - jumping

SHIFT - running

MOUSE1 - shoot (semi-automatic)

MOUSE2 - zoom

C - crouching

E - interact

F - dialogues

# References
https://youtu.be/vFvwyu_ZKfU?si=yA9Vb6HWxtZ1ars0

https://assetstore.unity.com/packages/3d/characters/modular-first-person-controller-189884

https://www.youtube.com/watch?v=XpG3YqUkCTY

https://www.youtube.com/playlist?list=PLrMEhC9sAD1zprGu_lphl3cQSS3uFIXA9

https://www.youtube.com/watch?v=Vh7wgvHZQBg

https://youtu.be/K06lVKiY-sY?si=D7TiARAffPQwZYlI

https://docs.unity3d.com/ScriptReference/Physics.Raycast.html

https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/interface

https://youtu.be/X-7A0WzSx5A?si=BIdfTcN3HRhKzvdr

https://www.youtube.com/watch?v=_nRzoTzeyxU

https://ollama.com

https://learn.microsoft.com/ru-ru/dotnet/api/system.collections.generic.linkedlistnode-1?view=net-8.0

https://youtu.be/BLfNP4Sc_iA?si=w3R9kvTaJLh79AoR

https://youtu.be/47ZkulgnadI?si=F0WkDD6bg0GO8mCn

https://youtu.be/DU7cgVsU2rM?si=9Tp7Q_4lAb1AWSMe

https://youtu.be/KZROVLPQdWc?si=du2qPup0-rbscDTV
