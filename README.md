# Fancy Button
Unity standard button replacement. With flexibly customizable components for different animations

## Usage

We add a Button component to the object, select PointerType, and register callbacks. Fine, it works.
But by default Button doesn't have any animations. Each animation type has its own component with its own settings. 

There are 5 types in total:

```
ButtonSpriteAnimator 
ButtonColorAnimator
ButtonScaleAnimator 
ButtonShineAnimator 
ButtonTextAnimator 
```

Each component requires settings that are created as a ScriptableObject. The path in the context menu to create `Create/ButtonSettings/`. All settings have one parameter in common - `State`. You need to set the states at which the animation will be triggered. There is no limitation on the number of elements in SO, so you can add several elements and set up a different animation for each state. You can combine the animations with each other and find the states you want. 

To handle the `Selected` state, in case you need to select a button from the group, you need to use the `Selectable` component. In it, just put a group of buttons on which you want to handle this state.

### Note
- There is no way to call methods that take any parameters when registering through the editor. But you can extend this functionality by creating custom Unity Events.
- You can use ButtonShineAnimator, but do not specify a prefab for the Shine sprite, the scala animation will work as well. 
