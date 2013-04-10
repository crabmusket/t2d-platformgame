function TestLevel::create(%this) {
   if(!isObject(GuiDefaultProfile)) {
      new GuiControlProfile(GuiDefaultProfile) {
         modal = true;
      };
   }

   if(!isObject(TheSceneWindow)) {
      new SceneWindow(TheSceneWindow);
      Canvas.setContent(TheSceneWindow);
   }

   TheSceneWindow.setCameraPosition(0, 0);
   TheSceneWindow.setCameraSize(20, 15);
   TheSceneWindow.setCameraZoom(1);
   TheSceneWindow.setCameraAngle(0);

   if(!isObject(TheScene)) {
      new Scene(TheScene);
      TheScene.setDebugOn("position", "oobb", "collision");
      TestLevel::construct(TheScene);
   }

   TheSceneWindow.setScene(TheScene);
}

function TestLevel::destroy(%this) {
   if(isObject(TheSceneWindow)) {
      TheSceneWindow.delete();
   }
   if(isObject(TheScene)) {
      TheScene.delete();
   }
}

function TestLevel::createRect(%w, %h) {
   %rect = new ShapeVector();
   %rect.setPolyPrimitive(4);
   %rect.setSize(%w SPC %h);
   %rect.setBodyType(static);
   %rect.createPolygonBoxCollisionShape(%w, %h);
   return %rect;
}

function TestLevel::construct(%scene) {
   %scene.setGravity(0, -9.8);

   // Create some platforms.
   %wall = TestLevel::createRect(15, 1);
   %wall.position = "0 -2";
   %scene.add(%wall);

   // Main character
   %player = PlatformCharacter::spawn("primary");
   %player.position = "0 2";
   %scene.add(%player);
   // And another
   %buddy = PlatformCharacter::spawn("secondary");
   %buddy.position = "2 2";
   %scene.add(%buddy);
}
