import {getRandomDate} from "./dateTimeFormat";

export class Config {
    static readonly Empty: Config = new Config("", new Date(0));

    readonly name: string;
    readonly path: string[];
    readonly updated: Date;
    readonly isValue: boolean;
    readonly value?: string

    constructor(path: string, updated: Date, value?: string) {
        this.path = path.split("/").filter(x => x);
        this.name = this.path[this.path.length - 1];
        this.updated = updated;
        this.value = value;
        this.isValue = !!value;
    }

    isEmpty() {
        return this === Config.Empty;
    }
}

type Source = { [zone: string]: Config[] };

const test: Source = {
    default: [
        new Config("configA", getRandomDate()),
        new Config("configA/configX", getRandomDate(), "some text"),
        new Config("configA/configY", getRandomDate()),
        new Config("configA/configY/biba", getRandomDate(), "boba"),
        new Config("configB", getRandomDate(), JSON.stringify({a: 10, b: 11})),
    ],
    production: [
        new Config("array", getRandomDate()),
        new Config("array/0", getRandomDate(), "first"),
        new Config("array/1", getRandomDate()),
        new Config("array/1/value", getRandomDate(), "text"),

        new Config("bigArray", getRandomDate()),
        ...Array(100).fill(null).map((_, i) => new Config("bigArray/" + i, getRandomDate(), "value " + i.toString()))
    ],
    staging: []
}

export class ApiClient {
    static async getTopZones(): Promise<string[]> {
        const zones = Object.keys(test);
        return await request(zones);
    }

    static async getConfigs(zone: string, path: string[] = []): Promise<Config[]> {
        const searchString = path.toString();
        const configs = test[zone].filter(config => config.path.length === path.length + 1 && config.path.toString().startsWith(searchString));
        return await request(configs);
    }
}

function request<T>(response: T): Promise<T> {
    return new Promise(resolve => {
        setTimeout(() => resolve(response), 1)
    });
}