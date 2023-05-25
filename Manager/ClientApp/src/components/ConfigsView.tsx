import ConfigRoute from "./ConfigsRoute";
import ConfigsTable from "./ConfigsTable";
import {Space, Typography} from "antd";
import React, {useEffect, useState} from "react";
import {ApiClient, Config} from "../utils/apiClient";
import EditConfigModal from "./EditConfigModal";

function ConfigsView({zone}: Props) {
    const [configs, setConfigs] = useState<Config[]>([]);
    const [path, setPath] = useState<string[]>([]);
    const [editConfig, setEditConfig] = useState<Config | null>(null);

    useEffect(() => {
        if (!zone)
            return;

        setPath([]);
        ApiClient.getConfigs(zone, []).then(x => setConfigs(x));
    }, [zone])

    async function selectConfig(config: Config) {
        if (!config.isValue)
            await changePath(config.path);
        else
            setEditConfig(config);
    }

    async function changePath(newPath: string[]) {
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
            <EditConfigModal open={!!editConfig} onClose={() => setEditConfig(null)} config={editConfig}/>
            <ConfigRoute zone={zone} path={path} onClick={i => changePath(path.slice(0, i))}/>
            <ConfigsTable
                path={path}
                configs={configs}
                onConfigSelect={selectConfig}
                onBackClick={() => changePath(path.slice(0, path.length - 1))}
            />
        </Space>
    )
}

interface Props {
    zone?: string;
}

export default ConfigsView;