import React, {useState} from "react";
import {Divider, Modal, Select, Space, Typography} from "antd";
import ConfigValueEditor, {ConfigValueFormat} from "./ConfigValueEditor";

const Text = Typography.Text;

const formats = [
    {value: 'Plain text', label: 'Plain text'},
    {value: 'JSON', label: 'JSON'},
    {value: 'YAML', label: 'YAML'},
]

function EditConfigValueModal({open, onClose, config}: Props) {
    const [format, setFormat] = useState<ConfigValueFormat>('Plain text')
    const [configValue, setConfigValue] = useState<string>(config.value)

    function onFormat(value: ConfigValueFormat) {
        setFormat(value)
    }

    return (
        <Modal
            title="Edit config value"
            open={open}
            onCancel={onClose}
            okText="Save"
            okButtonProps={{disabled: true}}
            destroyOnClose={true}
        >
            <div>
                <Text strong>Zone: </Text>
                <Text code>{config.zone}</Text><br/>
                <Text strong>Config key: </Text>
                <Text code>{config.key}</Text>
            </div>
            <Divider style={{margin: '5px 0 10px'}}/>
            <Space direction="vertical" style={{width: '100%'}}>
                <Space>
                    <Text strong>Format:</Text>
                    <Select style={{minWidth: '100px'}} options={formats} value={format} onChange={onFormat}/>
                </Space>
                <ConfigValueEditor format={format} value={configValue} onChange={setConfigValue}/>
            </Space>
        </Modal>
    )
}

interface Props {
    open: boolean;
    onClose: () => void;
    config: ConfigInfo;
}

interface ConfigInfo {
    zone: string;
    key: string;
    value: string;
}

export default EditConfigValueModal;