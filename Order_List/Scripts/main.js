

var app = new Vue({
    el: '#app',
    data: {
        url: {
            baseApi: "https://webtest.it.ua/testApp/api/",
            orders: "orders",
            statuses: "statuses",
            products: "products",
            add: "/add",
            update: "/update",
            delete: "/delete/",
        },
        dataStatuses: [],
        dataProducts: [],
        dataFull: [],
        deleteId: '',
        orderIndexInArray: '',
        modalSelect: '',
        modalPrice: 0,
        modalCount: 0,

        loading: true,
        errored: false,
    },
    computed:
    {
        modalSumm: function () {
            return this.modalCount * this.modalPrice;
        }
    },
    watch: {
        modalSelect: function () {
            this.modalPrice = this.dataProducts.find(product => product.id === this.modalSelect).price;
        }
    },
    mounted() {
        this.getOrders();
    },
    methods: {
        getOrders() {
            axios.all([
                axios.get(this.url.baseApi + this.url.orders),
                axios.get(this.url.baseApi + this.url.statuses),
                axios.get(this.url.baseApi + this.url.products)])
                .then(response => {
                    this.dataStatuses = response[1].data;
                    this.dataProducts = response[2].data;
                    this.getDataFullOrders(response[0].data);
                })
                .catch(error => {
                    console.log(error);
                    this.errored = true;
                })
                .finally(() => (this.loading = false));
        },
        addOrder() {
            console.log({ productId: this.modalSelect, count: this.modalCount });
            axios.post(this.url.baseApi + this.url.orders + this.url.add, { productId: this.modalSelect, count: this.modalCount })
                .then(response => {
                    this.pushOrderToArray(response.data);
                    console.log(response.data);
                    $("#add-order-modal").modal("hide");
                })
                .catch(error => { console.log(error); });
        },
        updateOrder() {
            let data = { id: 1, productId: this.modalSelect, statusId: 2, count: this.modalCount }
            axios.post(this.url.baseApi + this.url.orders + this.url.update, data)
                .then(response => {
                    console.log(response.data);
                })
                .catch(error => { console.log(error); });
        },
        deleteOrder() {
            axios.post(this.url.baseApi + this.url.orders + this.url.delete + this.deleteId)
                .then(response => {
                    this.dataFull.splice(this.orderIndexInArray, 1);
                    console.log(response.data);
                })
                .catch(error => { console.log(error); });
        },
        getDataFullOrders(ordersArray) {
            for (let i = 0; i < ordersArray.length; i++) {
                this.pushOrderToArray(ordersArray[i]);
            }
        },
        pushOrderToArray(obj) {
            this.dataFull.push({
                id: obj.id,
                count: obj.count,
                status: this.dataStatuses.find(status => status.id === obj.statusId),
                product: this.dataProducts.find(product => product.id === obj.productId),
            });
        },
        showAddOrderModal() {
            $("#add-order-modal").modal("show");
        },
        showDeleteModal(id, index) {
            $("#delete-modal").modal("show");
            console.log(index);
            this.deleteId = id;
            this.orderIndexInArray = index;
        },
    },
})
