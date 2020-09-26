# Paladin Coding Asignment
- Jirre Verkerk
- 26-09-2020
- Engine: Unity 2019.3.10f1
- Time: ~7 hours

## Features (Game feel focused)
- All player stats (jump-height, walking speed, gravity multiplier) are all easily accessible and edited for quick iteration
- Focused Camera view, the camera doesn't move with each of the player's moves, creating a certain sense of focus on the current 'room'. This can be used in future designs to create puzzles requiring the player to maybe backtrack or memorize other rooms for certain puzzles.
- 'endless' wrapping level, this feature can be expanded upon to not only wrap horizontal but also vertical for more exploratory feeling
- Screen shake to add impact to 'deaths' and 'pickups'

## Code Improvements
- I would implement Screen shake in the CameraManager instead of the GameManager due to it being more relevant to the CameraManager
- I prefer to comment my code more if I would have a bit more time to do so
- Implement custom editor scripts (or other ways to edit the inspectors) for ease of use by non-programmers; adding tooltips and bundling similar fields

## Features I would like to add (with more time)
- Being able to hold more than 1 key, using timers to make them disappear; this can allow puzzles where you need more than 1 key to solve them.
- More obstacles; currently the obstacles are all stationary, I would like some obstacles to move, as well as having a dynamic environment
- Level manipulation; I would like to use the wrapping in more ways, as well as altering the world by the actions of the players, e.g. changing gravity direction, slowly flooding a level, etc.

## Pre-written code
- JVFramework; some pieces of code I took from my own code library that weren't made during the 8 hour process, these can be found in the JVFramework folder

## Third-party
- Kenney - Simplified platform pack; Source: https://www.kenney.nl/assets/simplified-platformer-pack
- Unity3D 2d-extra-master (prefab brushes); Source: https://github.com/Unity-Technologies/2d-extras
