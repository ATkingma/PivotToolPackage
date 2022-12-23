Pivot Adjust 

Online forum available at:https://answers.unity.com/questions/1932735/form-of-detools-pivot-tool.html
E-mail: detoolsassetstore@gmail.com

1. Tool Flow:

a. hold ur mouse on the top left window tab.

b. open the pivot tool tab.

c. select an gameobject with an mesh filter.

d. follow the step 3.

e. follow step 4.

2. ABOUT
This tool helps you change the pivot point of an object without having to create an empty parent object as the pivot point. there is one way of adjusting the pivot:

a. If the object does have a meshFilter, then the script first creates an instance of the mesh, adjusts the mesh's pivot point by altering its vertices, normals and tangents, and finally changes the position of the current selected GameObject.



3. HOW TO set the pivot (there are multiple ways to set the pivot point):

A. To change an object's pivot point, enable the tool with the button "Enable Gizmo" or the button "/" and move the handle to the desired pivot position. Then, press the "set new pivot" button and the pivot wil be set.

B. you can also move the pivot at the movePivot tab here are some sliders that can be moved in the x,y,z directions you can changes the value of the slider scale at the settings tab. and then if u got your new pivot position you can press the set pivot position button.

C. you use the center button to center the pivot at the center of the current selected mesh. and then set the pivot with the set pivot button.

D. there is an last feature that is multiple pivot tool this uses more then 2 gui handles and the center of the amount of gui handles wil be calculated and if u got the desired position you can press on the set pivot position.



4. Exporting

you can export the mesh as the following file types:

-asset (.asset)
-fbx (.fbx)
-obj (.obj)
-stl (.stl)

5. Suggestions

Ive got some future plans for the tool but I need more time for this if you have some suggestions you can email me at the email at the top of the txt.

for the future:
 -collider bug fixes
 -support for skinned mesh renderers
 -2d support