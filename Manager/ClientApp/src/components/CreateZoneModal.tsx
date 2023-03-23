import {Modal, Space, Typography} from "antd";
import React from "react";
import ZoneSelect from "./ZoneSelect";
import Search from "antd/es/input/Search";

function CreateZoneModal({onClose}: Props) {

    return (
        <Modal
            title="Create zone"
            open={true}
            onCancel={onClose}
            okText="Create"
        >
            <Space direction="vertical" size="large" className="full-width">
                <Search placeholder="New zone name"/>
                <Space>
                    <Typography.Text strong>Parent zone</Typography.Text>
                    <ZoneSelect/>
                </Space>
            </Space>
        </Modal>
    )
}


interface Props {
    onClose: () => any
}

export default CreateZoneModal;