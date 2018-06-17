# IH_WearableDrone
[This Unity content is made as a part of a project in Inami-Hiyama Laboratory, University of Tokyo]

Source Link
https://www.evernote.com/l/ADjPoLJql95OaLLm2whEBqrmAvxS5wvwo8s

Video Link
https://www.youtube.com/watch?v=N1oxUehFfQo&feature=youtu.be

Thank you for trying Leviopole. This page contains a summarized instruction on how to run the software content for demonstration.
There are some important points to do in order to run this device, so please read this carefully.

##Equipment
1. VR-friendly PC running on Windows. For development we used GT62VR 7RE Dominator Pro.
2. HTC Vive, including two controllers, two Base Stations and one Tracker.
3. LevioPole
4. Headphone

##Environment
1. An open space and platforms where the two base stations can be set to monitor the space.

##Steps
1. Download or clone the git "IH_WearableDrone” that contains the software.
https://github.com/richard5635/IH_WearableDrone
2. This content runs on Unity 2017.02.0f3, so it is best to match it.
3. Open Unity Project.
4. In the root folder, there are several lists:
    * 3rd Party
    * Animation
    * Audio
    * Image
    * Material
    * Models
    * Prefabs
    * Scenes
    * Scripts
5. In the folder Scenes, open the desired scene. In the moment, the only content properly playable is PaddleRun02.
6. Open the scene, and now prepare the equipments for VR.
7. From here on are the necessary steps for HTC Vive. Some need only be done at first in a new environment, and some need to be done every time the content starts. 
8. HTC Vive - Initial Settings
    1. Connect the HTC Vive to the PC. Required steps can be seen here.
    2. Install HTC Vive Software. 
    3. Install Steam VR through Steam.
    4. Follow the calibration instructions. We used these settings for our development.


The tracker is placed on the floor, 
9. HTC Vive - Pre-game settings
    1. Two of the controllers must first be activated and recognized by the HMD first, then subsequently, the tracker must be connected. Open SteamVR Status, it shows which device are connected.
    2. We had troubles connecting the Tracker, so we would like to share it here.
Make sure the USB to the tracker is connected to a working USB hub. Then, open SteamVR Status, Right click on it, go to Devices > Pair Controller.
    3. When a new screen appears, turn the tracker on. The PC should now find the tracker and show it in the status.

In the end the status must show the devices shown in green.
10. All devices should show on the status with a green color without blinks.
11. Now, you should be ready to play, and start the game.


##In-Game Steps
1. While wearing the HMD, make sure you are located right on top of the canoe, and the tracker is tracking the paddle.
2. Due to the simple algorithm, as long as the paddle touches the water, the you will receive a pushing force forward. For fun experience, I suggest not to take advantage of this simple algorithm and paddle normally as how you would normally paddle in reality.
3. If you press M, a monster will spawn after a warning sign, and it will chase you from behind for a fixed time before disappearing again.
4. The game has a simple UI, which are
    1. HP Bar
    2. Speed Bar
    3. Score
    4. Time Limit
    5. Birdview Map
5. Once the time limit ends, you will lose control of your tracker, and the canoe would stop moving.
6. To replay the game, at the moment you would need to repress the play button again in Unity.
