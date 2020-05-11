import moment from "moment";

import { MOMENT_DATE_TIME_FORMAT_STRING } from "./Constants";

export function formatDateTime(dateTime: string): string {
    return moment(dateTime).format(MOMENT_DATE_TIME_FORMAT_STRING);
}
