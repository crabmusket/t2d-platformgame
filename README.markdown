# t2d-platformgame

This is my first game in [Torque 2D][].
It's an action-platformer for a friend who's doing the writing.
If you want to give it a go, see the instructions below!
Also, lots of the functionality is being developed in standalone modules.
Currently, these are:

 * [t2d-platformcharacter][] A physical 2D platformer character behavior.
 * [t2d-trackingcamera][] A more fully-featured camera tracking solution.
 * [t2d-damage][] A flexible damage effect system.

[![Basic features so far](http://img.youtube.com/vi/NRLBf25_L1o/0.jpg)][video]

_Click to see a video of the current gameplay features_

# Getting the game

I recommend cloning this repository directly.

```
git clone git@github.com:eightyeight/t2d-platformgame PlatformGame
cd PlatformGame
git submodule init
git submodule update
```

You'll then need to copy and paste the Torque2D executable (and DLLs on Windows) from somewhere.
The game is built against the `development` branch, but it should work with `master` branch releases.

[Torque 2D]: https://github.com/GarageGames/Torque2D
[t2d-platformcharacter]: https://github.com/eightyeight/t2d-platformcharacter
[t2d-trackingcamera]: https://github.com/eightyeight/t2d-trackingcamera
[t2d-damage]: https://github.com/eightyeight/t2d-damage
[video]: http://www.youtube.com/watch?v=NRLBf25_L1o
