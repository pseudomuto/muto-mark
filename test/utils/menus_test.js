"use strict";

import Menus from "utils/menus";

suite("utils/menus", () => {
  test("assets the truth", () => {
    assert.isTrue(new Menus().prop);
  });
});
