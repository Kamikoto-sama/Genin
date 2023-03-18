import React, {useState} from "react";
import {Divider, Input, Modal, Select, Space, Typography} from "antd";
import {JsonViewer} from "@textea/json-viewer";

const {Text} = Typography;

const testJsonValue = JSON.stringify({
    position: {
        x: 10,
        y: 20
    },
    valid: true,
    topology: [
        {
            name: "index1",
            address: "http://localhost"
        }
    ]
});

type ConfigValueFormat = 'Plain' | 'JSON' | 'YAML';
const formats = [
    {value: 'Plain', label: 'Plain'},
    {value: 'JSON', label: 'JSON'},
    {value: 'YAML', label: 'YAML'},
]

function ConfigValueEditor({format, value}: { format: ConfigValueFormat, value?: string }) {
    const placeholder = `Enter ${format} value (can be empty)`;
    switch (format) {
        case 'Plain':
            return <Input.TextArea placeholder={placeholder} defaultValue={value} autoSize/>
        case 'JSON':
            return <JsonViewer value={JSON.parse(value ?? '')} theme={"dark"}/>;
    }
    return (
        <>
            <Text type="warning" strong>Uknown format: </Text>
            <Text code>{format}</Text>
        </>
    )
}

function EditConfigValueModal({open, onClose, config}: Props) {

    const [format, setFormat] = useState<ConfigValueFormat>('Plain')

    function onFormat(value: ConfigValueFormat) {
        setFormat(value)
    }

    return (
        <Modal
            title="Edit config value"
            open={open}
            onCancel={onClose}
            okText="Save"
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
                <ConfigValueEditor format={format} value={testJsonValue}/>
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
}

export default EditConfigValueModal;