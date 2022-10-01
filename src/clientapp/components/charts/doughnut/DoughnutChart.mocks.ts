import { IDoughnutChart } from "./DoughnutChart";

const base: IDoughnutChart = {
  data: [
    {
      label: "Fats",
      data: 20,
      backgroundColor: "rgb(255, 0, 55)",
    },
    {
      label: "Proteins",
      data: 40,
      backgroundColor: "rgb(0, 153, 255)",
    },
    {
      label: "Proteins",
      data: 40,
      backgroundColor: "rgb(252, 180, 0)",
    },
  ],
};

export const mockDonghtChartProps = {
  base,
};
