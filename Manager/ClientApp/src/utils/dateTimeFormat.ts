const SECOND = 1000;
const MINUTE = SECOND * 60;
const HOUR = MINUTE * 60;
const DAY = HOUR * 24;
const WEEK = DAY * 7;
const MONTH = DAY * 30;
const YEAR = MONTH * 12;

function toTimeUnit(diff: number, unit: number, name: string) {
    const time = Math.floor(diff / unit);
    const plural = time > 1 ? "s" : "";
    return `${time} ${name}${plural} ago`;
}

export default function formatDate(date: Date): string {
    const diff = Math.abs(Date.now() - date.getTime());
    if (diff < 0)
        return `${date.toLocaleDateString("ru-RU")} ${date.toLocaleTimeString("ru-RU")}`
    if (diff < MINUTE)
        return "Less than a minute ago"
    if (diff < HOUR)
        return toTimeUnit(diff, MINUTE, "minute")
    if (diff < DAY)
        return toTimeUnit(diff, HOUR, "hour")
    if (diff < WEEK && diff / DAY < 2)
        return "Yesterday"
    if (diff < WEEK)
        return toTimeUnit(diff, DAY, "day")
    if (diff < MONTH)
        return toTimeUnit(diff, WEEK, "week")
    if (diff < YEAR)
        return toTimeUnit(diff, MONTH, "month")
    return toTimeUnit(diff, YEAR, "year")
}

export function getRandomDate() {
    const newDate = Date.now() - Math.random() * 10e10 % YEAR;
    return new Date(newDate);
}