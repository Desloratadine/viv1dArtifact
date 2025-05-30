local function create_item(id, name, description, effect)
    return {
        id = id,
        name = name,
        description = description,
        effect = effect
    }
end

local items = {
    list = {
        create_item("river_water", "河水", "掺杂了一些杂质的水...", "无"),
        create_item("mountain_spring", "山泉水", "清澈甘甜的山泉水...", "恢复少量生命值"),
        create_item("herb", "药草", "常见的草药...", "恢复少量状态")
    },
    by_id = {}
}


for _, item in ipairs(items.list) do
    items.by_id[item.id] = item
end

return items