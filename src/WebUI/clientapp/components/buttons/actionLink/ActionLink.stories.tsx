import { ComponentStory, ComponentMeta } from "@storybook/react";
import ActionLink, { IActionLink } from "./ActionLink";
import { mockActionLinkProps } from "./ActionLink.mocks";

export default {
  title: "buttons/ActionLink",
  component: ActionLink,
  // More on argTypes: https://storybook.js.org/docs/react/api/argtypes
  argTypes: {},
} as ComponentMeta<typeof ActionLink>;

// More on component templates: https://storybook.js.org/docs/react/writing-stories/introduction#using-args
const Template: ComponentStory<typeof ActionLink> = (args) => (
  <ActionLink {...args} />
);

export const Base = Template.bind({});
// More on args: https://storybook.js.org/docs/react/writing-stories/args

Base.args = {
  ...mockActionLinkProps.base,
} as IActionLink;
