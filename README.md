### Overview
This code repository holds a small number of scripts that I've recently made that show off my style of coding. Generally, I like to focus on smaller scripts that work together to accomplish a larger goal. I keep these scripts decoupled using game programming patterns like the Singleton, Observer, and Component patterns. While I do like to use these patterns, I try my best not to *over* use them, especially in cases where they add needless complexity. In particular, I'm very cautious about the use of the Singleton pattern, as I personally feel like it can get a bit out of hand if used freely.

I try to keep my code fairly well commented, but don't go crazy if I feel like a method or variable name describes itself in a good enough way. I personally like to leave a Description and Notes section in my scripts as well, just in case another person (or future me) wants a bit of extra guidance in parsing the script.
### Contents
- Two Scriptable Object classes to organize puzzle elements (Element.cs & Sequence.cs)
- Singleton Pattern used in the SequenceEmitter.cs for centralized data exchange
- Observer Pattern used in the SequenceEmitter.cs/SequenceListener.cs for decoupling
- Component Pattern used to activate iToggleActions (not included here) from SequenceListener.cs


