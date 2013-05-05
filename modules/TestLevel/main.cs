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
      TheScene.setDebugOn("oobb", "collision");
      TheSceneWindow.setScene(TheScene);
      TestLevel::construct(TheScene);
   }
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
   %scene.setGravity(0, -10);

   // Create some platforms.
   setRandomSeed(getRealTime());
   %i = 0;
   %totalWidth = 0;
   %num = getRandom(10, 30);
   while(%i < %num) {
      %width = getRandom(2, 5);
      %totalWidth += %width;
      %height = getRandom(1, 6);
      %walls[%i] = TestLevel::createRect(%width, %height);
      %scene.add(%walls[%i]);
      %i++;
   }
   %i = 0;
   %currentX = -1 * %totalWidth/2;
   while(%i < %num) {
      %walls[%i].position = %currentX + %walls[%i].getSizeX()/2 SPC %walls[%i].getSizeY()/2;
      %currentX += %walls[%i].getSizeX();
      %i++;
   }

   // Damage effects
   %e = Damage.createDefendEffect(HitTextBehavior);

   // Main character
   %player = PlatformCharacter::spawn("primary");
   %player.position = 0 SPC %walls[mFloor(%num/2)].getSizeY() + 2;
   Damage.effect(%player, %e);
   %scene.add(%player);
   // And another
   %buddy = PlatformCharacter::spawn("secondary");
   %buddy.position = 2 SPC %walls[mFloor(%num/2)].getSizeY() + 2;
   Damage.effect(%buddy, %e);
   %scene.add(%buddy);

   // Tracking camera
   TrackingCamera.addToWindow(TheSceneWindow);
   TrackingCamera.track(%player);
   TrackingCamera.track(%buddy);
}

function HitTextBehavior::onDamaged(%this, %attacker, %types, %amount, %impulse) {
   %t = new ImageFont();

   %t.image = "TestLevel:font";
   %t.fontSize = "0.2 0.4";
   %t.fontPadding = 0;
   %t.textAlignment = "Center";
   %t.text = %amount;

   %t.bodyType = "dynamic";
   %t.gravityScale = 0.3;
   %t.linearVelocity = "0 3";
   %t.position = %this.owner.getPosition();

   %t.schedule(1000, safeDelete);
   %this.owner.getScene().add(%t);

   Parent::onDamage(%this, %attacker, %defender, %types, %amount, %impulse);
}
