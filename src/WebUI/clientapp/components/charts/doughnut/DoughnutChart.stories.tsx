import { ComponentStory, ComponentMeta } from "@storybook/react";
import DoughnutChart, { IDoughnutChart } from "./DoughnutChart";
import { mockDonghtChartProps } from "./DoughnutChart.mocks";

export default {
  title: "charts/DoughnutChart",
  component: DoughnutChart,
  // More on argTypes: https://storybook.js.org/docs/react/api/argtypes
  argTypes: {},
} as ComponentMeta<typeof DoughnutChart>;

// More on component templates: https://storybook.js.org/docs/react/writing-stories/introduction#using-args
const Template: ComponentStory<typeof DoughnutChart> = (args) => (
  <DoughnutChart {...args} />
);

export const Base = Template.bind({});
// More on args: https://storybook.js.org/docs/react/writing-stories/args

Base.args = {
  ...mockDonghtChartProps.base,
} as IDoughnutChart
