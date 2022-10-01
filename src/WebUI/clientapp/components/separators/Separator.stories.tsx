import { ComponentStory, ComponentMeta } from "@storybook/react";
import Separator, { ISeparator} from "./Separator";
import { mockSeparatorProps } from "./Separator.mocks";

export default {
  title: "commons/Separator",
  component: Separator,
  // More on argTypes: https://storybook.js.org/docs/react/api/argtypes
  argTypes: {},
} as ComponentMeta<typeof Separator>;

// More on component templates: https://storybook.js.org/docs/react/writing-stories/introduction#using-args
const Template: ComponentStory<typeof Separator> = (args) => (
  <Separator {...args} />
);

export const Base = Template.bind({});
// More on args: https://storybook.js.org/docs/react/writing-stories/args

Base.args = {
  ...mockSeparatorProps.base,
} as ISeparator;
