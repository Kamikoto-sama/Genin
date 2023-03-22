import ConfigRoute from "./ConfigsRoute";
import ConfigsTable from "./ConfigsTable";
import {Space, Typography} from "antd";
import React, {useEffect, useState} from "react";
import {ApiClient, Config} from "../utils/apiClient";

function ConfigsView({zone}: Props) {
    const [configs, setConfigs] = useState<Config[]>([]);
    const [path, setPath] = useState<string[]>([])

    useEffect(() => {
        if (!zone)
            return;

        setPath([]);
        ApiClient.getConfigs(zone, []).then(x => setConfigs(x));
    }, [zone])

    async function selectConfig(newPath: string[]) {
        if (!zone)
            return;

        const configs = await ApiClient.getConfigs(zone, newPath);
        if (configs.length == 0)
            return;

        setPath(newPath);
        setConfigs(configs);
    }

    if (!zone)
        return <Typography.Text>Select zone</Typography.Text>;
    return (
        <Space direction="vertical" size="large" className="full-width">
            <ConfigRoute zone={zone} path={path} onClick={i => selectConfig(path.slice(0, i))}/>
            <ConfigsTable
                path={path}
                configs={configs}
                onConfigSelect={(c) => selectConfig(c.path)}
                onBackClick={() => selectConfig(path.slice(0, path.length - 1))}
            />
        </Space>
    )
}

interface Props {
    zone?: string;
}

export default ConfigsView;