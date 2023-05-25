import {Form, Input, Modal} from "antd";
import React, {useState} from "react";
import ZoneSelect from "./ZoneSelect";

function CreateZoneModal({open, onClose}: Props) {
    const [form] = Form.useForm()
    const [loading, setLoading] = useState(false);

    function onCreate() {
        setLoading(true);
        form.validateFields()
            .then(values => {
                alert(`Created: ${JSON.stringify(values)}`)
            })
            .catch(error => {
                alert(`Error: ${JSON.stringify(error)}`)
            })
            .finally(() => setLoading(false))
    }

    return (
        <Modal
            title="Create zone"
            open={open}
            onCancel={onClose}
            okText="Create"
            okButtonProps={{loading}}
            onOk={onCreate}
        >
            <Form
                form={form}
                labelCol={{span: 7}}
                wrapperCol={{span: 12}}
                labelAlign="left"
            >
                <Form.Item label="New zone name" name="newZoneName"
                           rules={[{required: true, message: "Zone name is required"}]}>
                    <Input/>
                </Form.Item>
                <Form.Item label="Parent zone name" name="parentZoneName">
                    <ZoneSelect/>
                </Form.Item>
            </Form>
        </Modal>
    )
}

interface Props {
    open: boolean;
    onClose: () => any
}

export default CreateZoneModal;