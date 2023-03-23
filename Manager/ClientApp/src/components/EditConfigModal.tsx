import React, {useState} from "react";
import {Button, Divider, Modal, Select, Space, Typography} from "antd";
import ConfigValueEditor, {ConfigValueFormat} from "./ConfigValueEditor";
import {Config} from "../utils/apiClient";

const Text = Typography.Text;

const formats = [
    {value: 'Plain text', label: 'Plain text'},
    {value: 'JSON', label: 'JSON'},
]

type FormatResult = { result: string, success: boolean };

function tryFormat<T>(source: string, parse: (source: string) => T, format: (parsed: T) => string): FormatResult {
    try {
        const parsed = parse(source);
        return {result: format(parsed), success: true};
    } catch (e: any) {
        return {result: e.message, success: false}
    }
}

function formatValue(format: ConfigValueFormat, value: string): FormatResult {
    switch (format) {
        case "JSON":
            return tryFormat(value, JSON.parse, (x) => JSON.stringify(x, null, 2))
        default:
            return {result: value, success: true};
    }
}

function EditConfigModal({onClose, config}: Props) {
    const [configFormat, setConfigFormat] = useState<ConfigValueFormat>('Plain text')
    const [configValue, setConfigValue] = useState(config.value ?? '')
    const [error, setError] = useState<string>();

    function formatConfigValue() {
        const result = formatValue(configFormat, configValue);
        if (!result.success)
            setError(result.result);
        else {
            setConfigValue(result.result);
            setError(undefined);
        }
    }

    return (
        <Modal
            title="Edit config value"
            open={true}
            onCancel={onClose}
            okText="Save"
            okButtonProps={{disabled: configValue === config.value}}
            destroyOnClose={true}
        >
            <div>
                <Text strong>Zone: </Text>
                <Text code>config.zone</Text><br/>
                <Text strong>Key: </Text>
                <Text code>{config.path.join("/")}</Text>
            </div>
            <Divider style={{margin: '5px 0 10px'}}/>
            <Space direction="vertical" className="full-width">
                <Space>
                    <Select style={{minWidth: '100px'}} options={formats} value={configFormat} onChange={setConfigFormat}/>
                    {configFormat !== 'Plain text' && <Button onClick={formatConfigValue}>Format</Button>}
                </Space>
                <ConfigValueEditor
                    format={configFormat}
                    value={configValue}
                    onChange={setConfigValue}
                    error={error}
                    onErrorClose={() => setError(undefined)}
                />
            </Space>
        </Modal>
    )
}

interface Props {
    onClose: () => void;
    config: Config;
}

export default EditConfigModal;