export function padTo2Digits(num: number) {
  return num.toString().padStart(2, "0");
}

export function formatDate(date: Date) {
  return [
    date.getFullYear(),
    padTo2Digits(date.getMonth() + 1),
    padTo2Digits(date.getDate()),
  ].join("-");
}

export function getCurrentWeekRange() {
  const lastDay = new Date();
  const firstDay = lastDay.setDate(lastDay.getDate() - 6);

  return {
    firstDay,
    lastDay,
  };
}
