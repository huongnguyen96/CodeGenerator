import React from "react";
import Card from "antd/lib/card";
import {addDecorator, configure} from "@storybook/react";
import "../src/index.scss";

addDecorator((storyFn) => {
  return (
    <Card>
      {storyFn()}
    </Card>
  );
});

configure(require.context("../src", true, /\.stories\.tsx?$/), module);
